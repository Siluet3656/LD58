using System;
using UnityEngine;
using Data;
using EntityResources;

namespace Battle
{
    [RequireComponent(typeof(Hp))]
    public class Enemy : MonoBehaviour, ITargetable
    {
        [SerializeField] private GameObject _spawnPoint;
        [SerializeField] private RandomSoundPlayer _randomSoundPlayer;
        [SerializeField] private RandomSoundPlayer _strikeSoundPlayer;
        
        private Hp _myHp;
        private EnemyAttack _enemyAttack;

        public bool IsNeedToGo = false;
        private bool isPlaying = false;
        private int _merchantSouls;
        
        private void Awake()
        {
            G.Enemies.Add(this);
            _myHp = GetComponent<Hp>();
            _enemyAttack = GetComponent<EnemyAttack>();
            IsTargetable = true;
            _merchantSouls = 0;
        }

        private void OnDisable()
        {
            OnTargetDie?.Invoke();
            G.Enemies.Remove(this);
            IsTargetable = false;
        }

        private void Update()
        {
            if (IsNeedToGo)
            {
                isPlaying = true;
                float step = 10f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _spawnPoint.transform.position, step);
            }

            if (isPlaying && IsNeedToGo == false)
            {
                isPlaying = false;
                _randomSoundPlayer.PlayRandomSound();
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

        public GameObject GameObject { get; }
        public event Action OnTargetDie;

        public void ApplyAbility(SkillType skillType)
        {
            if (BattleRuler.Instance.IsFighting == false) return;

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
                        //Sound
                        _myHp.TryToTakeDamage(AbilityDataCms.Instance.GetSpellConfig(skillType).damage, false);
                        G.SkillResources.ConsumeResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost);
                        _enemyAttack.Interrupt();
                        _enemyAttack.ApplyStun();
                    }
                    break;
            }
        }

        public void SetMerchantSouls(int amount)
        {
            _merchantSouls = amount;
        }
    }
}
