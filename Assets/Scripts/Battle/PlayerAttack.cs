using System.Collections;
using UnityEngine;
using EntityResources;
using UnityEngine.Serialization;
using View;

namespace Battle
{
    [RequireComponent(typeof(PlayerTargeting))]
    [RequireComponent(typeof(PlayerView))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _defaultAttackCooldownTime = 5f;
        [SerializeField] private float _defaultAttackDamage = 50f;
        [SerializeField] private RandomSoundPlayer _randomSoundPlayer;

        private float _currentAttackDamage;
        private float  _currentCooldownTime;
        
        private Hp _enemyHp;
        private PlayerTargeting _playerTargeting;
        private PlayerView _playerView;
        private SkillResources _skillResources;
        
        private float _currentSwipeProgress;
        private bool _isReadyToAttack;
        private int _energyRestorePerAttack = 0;
        private bool _isNeedTODoubled;
        private bool _isDoubled;

        private void Awake()
        {
            G.PlayerAttack = this;
            
            _playerTargeting = GetComponent<PlayerTargeting>();
            _playerView = GetComponent<PlayerView>();
            _randomSoundPlayer = GetComponent<RandomSoundPlayer>();
            _skillResources = GetComponent<SkillResources>();
            
            _isReadyToAttack = false;
            _isNeedTODoubled = false;
            _isDoubled = false;
            

            _currentAttackDamage = _defaultAttackDamage;
            _currentCooldownTime = _defaultAttackCooldownTime;
        }

        private void Update()
        {
            if (BattleRuler.Instance.IsFighting == false)
            {
                StopAllCoroutines();
                return;
            }

            if (_isNeedTODoubled)
            {
                if (_isDoubled == false)
                {
                    _currentAttackDamage *= 2;
                    _isDoubled = true;
                    G.PlayerView.UpdateAttackText((int)_currentAttackDamage);
                }
            }
            else
            {
                if (_isDoubled)
                {
                    _currentAttackDamage /= 2;
                    _isDoubled = false;
                    G.PlayerView.UpdateAttackText((int)_currentAttackDamage);
                }
            }
            
            PerformAttack();
        }

        private void OnEnable()
        {
            _playerTargeting.OnTargeted += SetTargetHp;
            BattleRuler.Instance.OnFighting += StartAttackCooldown;
            
            _currentSwipeProgress = 0f;
            _playerView.UpdateAttackSwingBar(_currentSwipeProgress);
        }

        private void OnDisable()
        {
            _playerTargeting.OnTargeted -= SetTargetHp;
            BattleRuler.Instance.OnFighting -= StartAttackCooldown;
            
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
            StartCoroutine(Cooldown(_currentCooldownTime));
        }

        private void PerformAttack()
        {
            if (_playerTargeting.HasTarget == false) return;
            
            if (_isReadyToAttack == false) return;
            
            if (_enemyHp == null) return;

            _playerView.StartAttackAnimation();
            
            _randomSoundPlayer.PlayRandomSound();
            
            _enemyHp.TryToTakeDamage(_currentAttackDamage, false);
            
            _skillResources.RestoreResources(_energyRestorePerAttack);
            
            StartAttackCooldown();
        }

        private void SetTargetHp(Transform target)
        {
            _enemyHp = target.GetComponent<Hp>();
        }

        public float CurrentDamage => _currentAttackDamage;
        public float DefaultDamage => _defaultAttackDamage;
        public float CurrentCooldownTime => _currentCooldownTime;
        public float DefaultCooldownTime => _defaultAttackCooldownTime;

        public void AdjustDamage(int amount)
        {
            if (amount < 1) return;
            
            _currentAttackDamage = amount;
        }

        public void SetUpEnergyRestorePerAttack(int amount)
        {
            _energyRestorePerAttack = amount;
        }

        public void SetUpDamageDoubling(bool isbloubled)
        {
            _isNeedTODoubled = isbloubled;
        }

        public void SetCooldownTime(float cooldown)
        {
            if (cooldown <= 0) return;
            
            _currentCooldownTime = cooldown;
        }
    }
}