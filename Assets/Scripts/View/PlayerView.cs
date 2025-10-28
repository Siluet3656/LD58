using System;
using EntityResources;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int AttackEnd = Animator.StringToHash("AttackEnd");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _energyBar;
        [SerializeField] private Text _attackText;
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

        private void OnEnable()
        {
            _animationEventCatcher.OnAttackEnd += SetTrigger;
            _animationEventCatcher.OnDeathAnimEnd += OnDeath;
            _myHp.OnDeath += () => _animator.SetTrigger(Death);
            _shieldAnimationEventCatcher.OnShieldAnimEnd += StopShieldAnimation;
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

        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }
        
        public void UpdateEnergyBar(int currentEnergy, int maxEnergy)
        {
            _energyBar.fillAmount = (float)currentEnergy/maxEnergy;
            _energyText.text = $"{currentEnergy}/{maxEnergy}";
        }

        public void UpdateAttackText(int currentAttack)
        {
            _attackText.text = $"{currentAttack}";
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
