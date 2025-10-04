using System;
using Data;
using EntityResources;
using UnityEngine;

namespace Battle
{
    [RequireComponent(typeof(Hp))]
    public class Enemy : MonoBehaviour, ITargetable
    {
        private Hp _myHp;
        private EnemyAttack _enemyAttack;
        private void Awake()
        {
            G.Enemies.Add(this);
            _myHp = GetComponent<Hp>();
            _enemyAttack = GetComponent<EnemyAttack>();
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
                    {
                        _myHp.TryToTakeDamage(AbilityDataCms.Instance.GetSpellConfig(skillType).damage, false);
                        G.SkillResources.ConsumeResources(AbilityDataCms.Instance.GetSpellConfig(skillType).cost);
                        _enemyAttack.Interrupt();
                    }
                    break;
            }
        }
    }
}
