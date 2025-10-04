using System;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class PlayerTargeting : MonoBehaviour
    {
        [SerializeField] private ArrowToTarget _arrowToTarget;
        
        private ITargetable _currentTarget;
        
        private void Start()
        {
            SetTarget(G.Enemies.First());
        }

        private void SetTarget(ITargetable target)
        {
            target.OnTargeted();
            target.OnTargetDie += ClearTarget;
            _currentTarget = target;

            if (_currentTarget is Enemy enemy)
            {
                _arrowToTarget.SetupArrow(transform, enemy.gameObject.transform);
                OnTargeted?.Invoke(enemy.gameObject.transform);
            }
        }
        
        private void ClearTarget()
        {
            if (HasTarget)
            {
                _currentTarget.OnUntargeted();
                _currentTarget.OnTargetDie -= ClearTarget;
                _currentTarget = null;
            }
        }
        
        public bool HasTarget => _currentTarget != null;
        public ITargetable GetCurrentTarget => _currentTarget;

        public Action<Transform> OnTargeted;
        
        public void OnFastTarget()
        {
            //ITargetable target;
            
            //SetTarget(target);
        }

        public void OnMouseTargetSelect(RaycastHit2D hit)
        {
            ClearTarget();

            if (hit.collider != null && hit.collider.TryGetComponent<ITargetable>(out var target))
            {
                SetTarget(target);
            }
        }
    }
}