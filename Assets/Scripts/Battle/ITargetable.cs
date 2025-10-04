using System;
using UnityEngine;

namespace Battle
{
    public interface ITargetable
    {
        bool IsTargetable { get; }
        bool IsTargeted { get; }
    
        void OnTargeted();
        void OnUntargeted();
        
        GameObject GameObject { get; }

        event Action OnTargetDie;
    }
}