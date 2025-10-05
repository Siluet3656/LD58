using System;
using Data;
using EntityResources;
using UnityEngine;

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
        
        private void Awake()
        {
            G.Enemies.Add(this);
            _myHp = GetComponent<Hp>();
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        private void OnDestroy()
        {
            G.Enemies.Remove(this);
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

        public bool IsTargetable { get; }
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
            
            switch (skillType)
            {
                case SkillType.Strike:
                    if (G.SkillResources.HasEnoughResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost))
                    {   _strikeSoundPlayer.PlayRandomSound();
                        _myHp.TryToTakeDamage(AbilityDataCms.Instance.GetSpellConfig(skillType).damage, false);
                        G.SkillResources.ConsumeResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost);
                        _enemyAttack.Interrupt();
                    }
                    break;
            }
        }
    }
}
