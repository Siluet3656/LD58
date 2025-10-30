using System;
using UnityEngine;
using EntityResources;

namespace Battle
{
    public class PlayerTargeting : MonoBehaviour
    {
        [SerializeField] private ArrowToTarget _arrowToTarget;
        
        private ITargetable _currentTarget;

        private void OnEnable()
        {
            BattleRuler.Instance.OnFighting += AutoTarget;
        }
        
        private void OnDisable()
        {
            BattleRuler.Instance.OnFighting -= AutoTarget;
        }
        
        private void Start()
        {
            foreach (var enemy in G.Enemies)
            {
                enemy.GetComponent<Enemy>().OnTargetDie += AutoTarget;
                enemy.GetComponent<Hp>().OnDeath += () => _arrowToTarget.SetupArrow(transform, null);
                enemy.GetComponent<Enemy>().OnRetreat += () => _arrowToTarget.SetupArrow(transform, null);
                enemy.GetComponent<Enemy>().OnRetreat += AutoTarget;
            }
        }

        private void OnDestroy()
        {
            G.Enemies.Clear();
        }

        private void SetTarget(ITargetable target)
        {
            target.OnTargeted();
            _currentTarget = target;

            if (_currentTarget is Enemy enemy)
            { 
                if (enemy.IsTargetable == false) return;

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
                _arrowToTarget.SetupArrow(null, null);
            }
        }
        
        private void AutoTarget()
        {
            foreach (Enemy enemy in G.Enemies)
            {
                if (enemy.isActiveAndEnabled)
                {
                    SetTarget(enemy);
                    return;
                }
            }
            
            ClearTarget();
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
            //ClearTarget();

            if (hit.collider != null && hit.collider.TryGetComponent<ITargetable>(out var target))
            {
                SetTarget(target);
            }
        }

        public void ClearArrow()
        {
            _arrowToTarget.Clear();
        }
    }
}