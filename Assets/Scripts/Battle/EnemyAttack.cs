using System.Collections;
using UnityEngine;
using EntityResources;
using View;

namespace Battle
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _attackCooldownTime = 3f;
        [SerializeField] private float _attackDamage = 1f;
        
        private Hp _playerHp;
        private EnemyView _myView;
        
        private float _currentSwipeProgress;
        private bool _isReadyToAttack;
        private float _elapsedTime;
        
        private void Awake()
        {
            _myView = GetComponent<EnemyView>();
            
            _isReadyToAttack = false;
        }

        private void Start()
        {
            _playerHp = G.Player.GetComponent<Hp>();
            _myView.UpdateDamage(_attackDamage);
        }

        private void Update()
        {
            if (BattleRuler.Instance.IsFighting == false)
            {
                StopAllCoroutines();
                return;
            }
            
            PerformAttack();
        }

        private void OnEnable()
        {
            BattleRuler.Instance.OnFighting += StartAttackCooldown;
            
            _currentSwipeProgress = 0f;
            _myView.UpdateAttackSwingBar(_currentSwipeProgress);
        }

        private void OnDisable()
        {
            BattleRuler.Instance.OnFighting -= StartAttackCooldown;
            
            StopAllCoroutines();
        }

        private IEnumerator Cooldown(float cooldown)
        {
            _isReadyToAttack = false;
            _elapsedTime = 0f;
            _currentSwipeProgress = 0f;

            while (_elapsedTime < cooldown)
            {
                _elapsedTime += Time.deltaTime;
                _currentSwipeProgress = Mathf.Clamp01(_elapsedTime / cooldown);
                _myView.UpdateAttackSwingBar(_currentSwipeProgress);
                yield return null;
            }

            _isReadyToAttack = true;
            _currentSwipeProgress = 1f;
            _myView.UpdateAttackSwingBar(_currentSwipeProgress);
        }

        private void StartAttackCooldown()
        {
            StartCoroutine(Cooldown(_attackCooldownTime));
        }

        private void PerformAttack()
        {
            if (_isReadyToAttack == false) return;
            
            if (_playerHp == null) return;

            _playerHp.TryToTakeDamage(_attackDamage, false);
            
            StartAttackCooldown();
        }

        public void Interrupt()
        {
            _elapsedTime = 0f;
        }
    }
}