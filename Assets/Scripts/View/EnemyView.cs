using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        private static readonly int AttackEnd = Animator.StringToHash("AttackEnd");
        private static readonly int Attack = Animator.StringToHash("Attack");
        [SerializeField] private Image _swingBar;
        [SerializeField] private Text _textDamage;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventCatcher _animationEventCatcher;
        
        private void SetTrigger()
        {
            _animator.SetTrigger(AttackEnd);
        }
        
        private void OnEnable()
        {
            _animationEventCatcher.OnAttackEnd += SetTrigger;
        }

        private void OnDisable()
        {
            _animationEventCatcher.OnAttackEnd -= SetTrigger;
        }
        
        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }

        public void UpdateDamage(float currentDamage)
        {
            _textDamage.text = $"{currentDamage}";
        }
        
        public void StartAttackAnimation()
        {
            _animator.SetTrigger(Attack);
        }
    }
}
