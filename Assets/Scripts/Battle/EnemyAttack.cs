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
        [SerializeField] private float _attackDamage = 1f;
        [SerializeField] private RandomSoundPlayer _randomSoundPlayer;
        [SerializeField] private Image _interruptSprite;
        
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

            _myView.StartAttackAnimation();
            
            _randomSoundPlayer.PlayRandomSound();
            
            _playerHp.TryToTakeDamage(_attackDamage, false);
            
            StartAttackCooldown();
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

        public void Interrupt()
        {
            _elapsedTime = 0f;
            
            Vector3 randPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y + Random.Range(-1f, -2f),transform.position.z);
            
            DamagePopup.Instance.AddText("interrupt!", randPosition, Color.red);

            StartCoroutine(InterruptAnimation());
        }
    }
}