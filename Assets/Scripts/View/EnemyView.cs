using EntityResources;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        private static readonly int AttackEnd = Animator.StringToHash("AttackEnd");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _stunSprite;
        [SerializeField] private Text _textDamage;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventCatcher _animationEventCatcher;
        [SerializeField] private Hp _myHp;
        
        private void SetTrigger()
        {
            _animator.SetTrigger(AttackEnd);
        }
        
        private void OnEnable()
        {
            _animationEventCatcher.OnAttackEnd += SetTrigger;
            _animationEventCatcher.OnDeathAnimEnd += () => gameObject.SetActive(false);
            _myHp.OnDeath += () => _animator.SetTrigger(Death);
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

        public void UpdateStunCircle(float currentStunProgress)
        {
            _stunSprite.fillAmount = currentStunProgress;
        }
    }
}
