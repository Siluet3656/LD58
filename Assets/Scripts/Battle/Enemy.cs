using System;
using UnityEngine;

namespace Battle
{
    public class Enemy : MonoBehaviour, ITargetable
    {
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
    }
}
