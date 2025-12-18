using System;
using UnityEngine;
using Data;
using EntityResources;
using View;

namespace Battle
{
    [RequireComponent(typeof(Hp))]
    public class Enemy : MonoBehaviour, ITargetable
    {
        [SerializeField] private GameObject _spawnPoint;
        [SerializeField] private RandomSoundPlayer _randomSoundPlayer;
        [SerializeField] private RandomSoundPlayer _strikeSoundPlayer;
        [SerializeField] private RandomSoundPlayer _punchSoundPlayer;

        [Header("Traits")] 
        [SerializeField] private int _steady;
        [SerializeField] private int _greedy;
        [SerializeField] private int _hopeful;
        [SerializeField] private int _fury;
        [SerializeField] private int _hateful;
        [SerializeField] private int _leader;
        [SerializeField] private int _wellTrained;
        [SerializeField] private int _scientist;
        [SerializeField] private int _pure;
        [SerializeField] private int _defiled;
        [SerializeField] private int _artificial;
        
        [Header("Retreat")]
        [SerializeField] private bool _retreat;
        [SerializeField, Range(0f,1f)] private float _retreatHealthPercentage;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Hp _myHp;
        private EnemyAttack _enemyAttack;
        private EnemyView _myView;

        public bool IsNeedToGo = false;
        private bool isPlaying = false;
        private int _merchantSouls;
        private int _followerSouls;
        private bool _isRetreatStarted = false;
        
        private void Awake()
        {
            G.Enemies.Add(this);
            _myHp = GetComponent<Hp>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _myView = GetComponent<EnemyView>();
            IsTargetable = true;
            _merchantSouls = 0;
            _followerSouls = 0;
            GameObject = gameObject;
            
            _myHp.OnDeath += () => OnTargetDie?.Invoke();
        }

        private void OnDisable()
        {
            G.Enemies.Remove(this);
            IsTargetable = false;
        }

        private void Update()
        {
            if (IsNeedToGo)
            {
                if (_spawnPoint != null)
                {
                    isPlaying = true;
                    float step = 15f * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, _spawnPoint.transform.position, step);
                }
            }

            if (isPlaying && IsNeedToGo == false)
            {
                isPlaying = false;
                _randomSoundPlayer.PlayRandomSound();
            }

            if (BattleRuler.Instance.IsFighting == false)
            {
                UpdateEnemyStatus();
            }

            if (_retreat && _myHp.CurrentHealth <= _myHp.MaxHealth * _retreatHealthPercentage)
            {
                if (_isRetreatStarted == false)
                {
                    DamagePopup.Instance.AddText("Retreat!!",_spawnPoint.transform.position, Color.white);
                    _spriteRenderer.flipX = true;
                    _myHp.GetInvulnerable();
                    OnRetreat?.Invoke();
                    G.Enemies.Remove(this);
                    IsNeedToGo  = false;
                    _isRetreatStarted = true;
                    G.Player.GetComponent<PlayerTargeting>().ClearArrow();
                }
                
                float step = 5f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_spawnPoint.transform.position.x + 500f, 
                    _spawnPoint.transform.position.y,
                    _spawnPoint.transform.position.z), step);
            }
        }

        private void UpdateEnemyStatus()
        {
            _enemyAttack.SetRestoreHpByAttackAmount(_steady * 2f);
            _enemyAttack.StealEnergyByAttackAmount(_greedy * 10f);
            _myHp.SetHealAfterHealthDrop(_hopeful);
            
            if (_scientist > 0)
            {
                _myHp.SetRegenerate(true);
            }

            if (_pure > 0)
            {
                _myHp.SetRegenerate(true);
                _myHp.SetRegenerateAmount((int)(_myHp.MaxHealth * 0.01f));
            }

            _enemyAttack.SetFury(_fury);

            if (_hateful > 0 && _followerSouls > 0)
            {
                _myHp.LoseHpAtStart(true);
            }
            
            _enemyAttack.SetHealthOnHitConsumed(_defiled * 2f);

            if (_artificial > 0)
            {
                _myHp.SetMaxHealth(1);
                _myHp.GetInvulnerable();
                _myHp.LoseInvulerabiltyWhenAllAliesDead();
            }
        }

        public bool IsTargetable { get; private set; }
        public bool IsTargeted { get; }
        public void OnTargeted()
        {
            
        }

        public void OnUntargeted()
        {
            
        }

        public GameObject GameObject { get; private set; }
        public event Action OnTargetDie;
        public event Action OnRetreat;

        public int MerchantSouls => _merchantSouls;

        public void ApplyAbility(SkillType skillType)
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            switch (skillType)
            {
                case SkillType.Strike:
                    if (G.SkillResources.HasEnoughResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost))
                    {   
                        _strikeSoundPlayer.PlayRandomSound();
                        _myHp.TryToTakeDamage(AbilityDataCms.Instance.GetSpellConfig(skillType).damage, false);
                        G.SkillResources.ConsumeResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost);
                        _enemyAttack.Interrupt();
                    }
                    break;
                case SkillType.Punch:
                    if (G.SkillResources.HasEnoughResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost))
                    {
                        _punchSoundPlayer.PlayRandomSound();
                        _myHp.TryToTakeDamage(AbilityDataCms.Instance.GetSpellConfig(skillType).damage, false);
                        G.SkillResources.ConsumeResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost);
                        _enemyAttack.Interrupt();
                        _enemyAttack.ApplyStun();
                        _myView.StartStunAnimation();
                    }
                    break;
            }
            
            if (skillType != SkillType.None)
            {
                G.PlayerHp.RestoreHpForKnightSouls();

                foreach (Enemy enemy in BattleRuler.Instance.EnemiesOnScene)
                {
                    Hp hp = enemy.GetComponent<Hp>();
                    hp.TryToTakeDamage(2 * _merchantSouls, false);
                }
                G.PlayerHp.TryToTakeDamage(2 * _merchantSouls, false);
            }
        }

        public void SetMerchantSouls(int amount)
        {
            _merchantSouls = amount;
        }

        public void SetFollowerSouls(int amount)
        {
            _followerSouls = amount;
        }
    }
}
