using System;
using System.Collections;
using UnityEngine;
using EntityResources;
using View;

namespace Battle
{
    [RequireComponent(typeof(PlayerTargeting))]
    [RequireComponent(typeof(PlayerView))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _attackCooldownTime = 5f;
        [SerializeField] private float _attackDamage = 50f;
        
        private Hp _enemyHp;
        private PlayerTargeting _playerTargeting;
        private PlayerView _playerView;
        
        private float _currentSwipeProgress;
        private bool _isReadyToAttack;
        
        private void Awake()
        {
            _playerTargeting = GetComponent<PlayerTargeting>();
            _playerView = GetComponent<PlayerView>();
            
            _isReadyToAttack = false;
            StartAttackCooldown();
        }

        private void Update()
        {
            PerformAttack();
        }

        private void OnEnable()
        {
            _playerTargeting.OnTargeted += SetTargetHp;
            
            _currentSwipeProgress = 0f;
            _playerView.UpdateAttackSwingBar(_currentSwipeProgress);
        }

        private void OnDisable()
        {
            _playerTargeting.OnTargeted -= SetTargetHp;
            
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

        private void SetTargetHp(Transform target)
        {
            _enemyHp = target.GetComponent<Hp>();
        }
    }
}