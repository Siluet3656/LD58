using System.Collections;
using UnityEngine;
using EntityResources;
using UnityEngine.UI;
using View;

namespace Battle
{
    [RequireComponent(typeof(EnemyView))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _attackCooldownTime = 3f;
        [SerializeField] private float _defaultDamage = 1f;
        [SerializeField] private RandomSoundPlayer _randomSoundPlayer;
        [SerializeField] private Image _interruptSprite;
        
        private Hp _playerHp;
        private EnemyView _myView;
        private Hp _myHp;
        
        private float _currentSwipeProgress;
        private bool _isReadyToAttack;
        private float _elapsedTime;
        private bool _isStunned;
        private Coroutine _stunCoroutine;
        private Coroutine _cooldownCoroutine;
        
        private float _stunDuration = 3f;

        private float _restoreHpByAttack;
        private float _stealEnergyByAttack;

        private float _attackDamage;
        private int _furyTraits;

        private float _healthOnHitConsumed;
        
        private bool _isCanAttack;
        
        private void Awake()
        {
            _myView = GetComponent<EnemyView>();
            _myHp = GetComponent<Hp>();
            
            _isReadyToAttack = false;
            _isCanAttack = true;
            
            _attackDamage = _defaultDamage;
            _furyTraits = 0;
            _healthOnHitConsumed = 0;
        }

        private void Start()
        {
            _playerHp = G.Player.GetComponent<Hp>();
            _myView.UpdateDamage(_attackDamage, _attackCooldownTime);
        }

        private void Update()
        {
            if (BattleRuler.Instance.IsFighting == false)
            {
                StopAllCoroutines();
                return;
            }

            if (_furyTraits > 0)
            {
                if (_myHp.CurrentHealth <= _myHp.MaxHealth * 0.5f)
                {
                    SetDamage(_defaultDamage * 2f * _furyTraits);
                }
                else
                {
                    SetDamage(_defaultDamage);
                }
            }

            if (_isCanAttack)
            {
                TryToPerformAttack();
            }
        }

        private void OnEnable()
        {
            BattleRuler.Instance.OnFighting += StartAttackCooldown;
            
            _currentSwipeProgress = 0f;
            _myView.UpdateAttackSwingBar(_currentSwipeProgress);

            _myHp.OnAnyDamageReceived += OnTakeDamage;
            GetComponent<Enemy>().OnRetreat += () => _isCanAttack = false;
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
                if (_isStunned)
                {
                    ResetAttackProgress();
                    yield break;
                }

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
            if (_isStunned) return;

            if (_cooldownCoroutine != null)
                StopCoroutine(_cooldownCoroutine);

            _cooldownCoroutine = StartCoroutine(Cooldown(_attackCooldownTime));
        }

        private void TryToPerformAttack()
        {
            if (_isReadyToAttack == false) return;
            
            if (_playerHp == null) return;

            _myView.StartAttackAnimation();
            
            _randomSoundPlayer.PlayRandomSound();
            
            _playerHp.TryToTakeDamage(_attackDamage, false);
            _myHp.Heal(_restoreHpByAttack);
            G.SkillResources.ConsumeResources((int)_stealEnergyByAttack);
            _myHp.TryToTakeDamage(_healthOnHitConsumed,false);
            
            StartAttackCooldown();
        }
        
        private void ResetAttackProgress()
        {
            _isReadyToAttack = false;
            _currentSwipeProgress = 0f;
            _myView.UpdateAttackSwingBar(_currentSwipeProgress);

            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
                _cooldownCoroutine = null;
            }
        }

        private IEnumerator StunRoutine()
        {
            _isStunned = true;
            ResetAttackProgress();

            float elapsed = 0f;
            while (elapsed < _stunDuration)
            {
                elapsed += Time.deltaTime;
                _myView.UpdateStunCircle(1f - elapsed / _stunDuration);
                yield return null;
            }

            RemoveStun();
        }

        private IEnumerator InterruptAnimation()
        {
            float time = 0f;

            while (_elapsedTime < 1f)
            {
                time += Time.deltaTime;
                _interruptSprite.color = new Color(_interruptSprite.color.r, _interruptSprite.color.g,
                    _interruptSprite.color.b, 1f - time);
                yield return null;
            }
        }

        public float DefaultDamage => _defaultDamage;

        public void Interrupt()
        {
            _elapsedTime = 0f;
            
            Vector3 randPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y + Random.Range(-1f, -2f),transform.position.z);
            
            DamagePopup.Instance.AddText("interrupt!", randPosition, Color.red);

            StartCoroutine(InterruptAnimation());
        }
        
        public void RemoveStun()
        {
            _isStunned = false;
            if (_stunCoroutine != null)
            {
                StopCoroutine(_stunCoroutine);
                _stunCoroutine = null;
            }

            StartAttackCooldown();
            _myView.UpdateStunCircle(0);
        }

        public void OnTakeDamage(float f)
        {
            if (_isStunned)
            {
                RemoveStun();
            }

        }
        
        public void ApplyStun()
        {
            if (_stunCoroutine != null)
                StopCoroutine(_stunCoroutine);
                
            _myView.UpdateStunCircle(1);
            _stunCoroutine = StartCoroutine(StunRoutine());
        }

        public void SetRestoreHpByAttackAmount(float amount)
        {
            _restoreHpByAttack = amount;
        }

        public void StealEnergyByAttackAmount(float amount)
        {
            _stealEnergyByAttack = amount;
        }

        public void SetDamage(float amount)
        {
            _attackDamage = amount;
            _myView.UpdateDamage(_attackDamage, _attackCooldownTime);
        }

        public void SetFury(int amount)
        {
            _furyTraits = amount;
        }

        public void SetHealthOnHitConsumed(float amount)
        {
            _healthOnHitConsumed = amount;
        }
    }
}