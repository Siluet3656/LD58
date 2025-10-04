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
        
        private Hp _enemyHp;
        private EnemyView _playerView;
        
        private float _currentSwipeProgress;
        private bool _isReadyToAttack;
        
        private void Awake()
        {
            _playerView = GetComponent<EnemyView>();
            
            _isReadyToAttack = false;
            StartAttackCooldown();
        }

        private void Start()
        {
            _enemyHp = G.Player.GetComponent<Hp>();
        }

        private void Update()
        {
            PerformAttack();
        }

        private void OnEnable()
        {
            _currentSwipeProgress = 0f;
            _playerView.UpdateAttackSwingBar(_currentSwipeProgress);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Cooldown(float cooldown)
        {
            _isReadyToAttack = false;
            float elapsedTime = 0f;
            _currentSwipeProgress = 0f;

            while (elapsedTime < cooldown)
            {
                elapsedTime += Time.deltaTime;
                _currentSwipeProgress = Mathf.Clamp01(elapsedTime / cooldown);
                _playerView.UpdateAttackSwingBar(_currentSwipeProgress);
                yield return null;
            }

            _isReadyToAttack = true;
            _currentSwipeProgress = 1f;
            _playerView.UpdateAttackSwingBar(_currentSwipeProgress);
        }

        private void StartAttackCooldown()
        {
            StartCoroutine(Cooldown(_attackCooldownTime));
        }

        private void PerformAttack()
        {
            if (_isReadyToAttack == false) return;
            
            if (_enemyHp == null) return;

            _enemyHp.TryToTakeDamage(_attackDamage, false);
            
            StartAttackCooldown();
        }
    }
}