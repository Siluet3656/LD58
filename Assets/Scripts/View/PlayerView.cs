using EntityResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int AttackEnd = Animator.StringToHash("AttackEnd");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int HitEnd = Animator.StringToHash("HitEnd");
        
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _energyBar;
        [SerializeField] private TMP_Text _attackText;
        [SerializeField] private Text _energyText;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventCatcher _animationEventCatcher;
        [SerializeField] private GameObject _shield;
        
        private Hp _myHp;
        private AnimationEventCatcher _shieldAnimationEventCatcher;

        private void Awake()
        {
            G.PlayerView = this;
            _myHp = GetComponent<Hp>();
            _shieldAnimationEventCatcher = _shield.GetComponent<AnimationEventCatcher>();
        }

        private void SetTrigger()
        {
            _animator.SetTrigger(AttackEnd);
        }
        
        private void SetTriggerHitEnd()
        {
            _animator.SetTrigger(HitEnd);
        }

        private void OnEnable()
        {
            _animationEventCatcher.OnAttackEnd += SetTrigger;
            _animationEventCatcher.OnDeathAnimEnd += OnDeath;
            _animationEventCatcher.OnHitAnimEnd += SetTriggerHitEnd;
            
            _myHp.OnDeath += () => _animator.SetTrigger(Death);
            _shieldAnimationEventCatcher.OnShieldAnimEnd += StopShieldAnimation;
            
            _myHp.OnAnyDamageReceived += dmg => StartHitAnimation();
        }

        private void OnDisable()
        {
            _animationEventCatcher.OnAttackEnd -= SetTrigger;
            _animationEventCatcher.OnDeathAnimEnd -= OnDeath;
            _shieldAnimationEventCatcher.OnShieldAnimEnd -= StopShieldAnimation;
        }

        private void OnDeath()
        {
            gameObject.SetActive(false);
        }
        
        public void StartHitAnimation()
        {
            _animator.SetTrigger(Hit);
        }

        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }
        
        public void UpdateEnergyBar(int currentEnergy, int maxEnergy)
        {
            _energyBar.fillAmount = (float)currentEnergy/maxEnergy;
            _energyText.text = $"{currentEnergy}/{maxEnergy}";
        }

        public void UpdateAttackText(int currentAttack, float currentAttackCooldown)
        {
            _attackText.text = $"{currentAttack}\n---\n{currentAttackCooldown}";
        }

        public void StartAttackAnimation()
        {
            _animator.SetTrigger(Attack);
        }

        public void PlayShieldAnimation()
        {
            _shield.SetActive(true);
        }

        public void StopShieldAnimation()
        {
            _shield.SetActive(false);
        }
    }
}
