using System;
using EntityResources;
using UnityEngine;

namespace Battle
{
    [RequireComponent(typeof(Hp))]
    public class Enemy : MonoBehaviour, ITargetable
    {
        private Hp _myHp;
        private void Awake()
        {
            G.Enemies.Add(this);
            _myHp = GetComponent<Hp>();
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

        public void ApplyAbility()
        {
            _myHp.TryToTakeDamage(1f, false);   
        }
    }
}
