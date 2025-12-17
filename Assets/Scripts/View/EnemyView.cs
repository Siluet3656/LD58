using EntityResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        private static readonly int AttackEnd = Animator.StringToHash("AttackEnd");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int HitEnd = Animator.StringToHash("HitEnd");
        private static readonly int Stun = Animator.StringToHash("Stun");
        private static readonly int StunEnd = Animator.StringToHash("StunEnd");
        private static readonly int DialogueEnd = Animator.StringToHash("DialogueEnd");
        
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _stunSprite;
        [SerializeField] private TMP_Text _textDamage;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventCatcher _animationEventCatcher;
        [SerializeField] private Hp _myHp;
        
        private void SetTriggerAttackEnd()
        {
            _animator.SetTrigger(AttackEnd);
        }
        
        private void SetTriggerHitEnd()
        {
            _animator.SetTrigger(HitEnd);
        }
        
        private void SetTriggerStunEnd()
        {
            _animator.SetTrigger(StunEnd);
        }
        
        private void OnEnable()
        {
            _animationEventCatcher.OnAttackEnd += SetTriggerAttackEnd;
            _animationEventCatcher.OnHitAnimEnd += SetTriggerHitEnd;
            _animationEventCatcher.OnStunAnimEnd += SetTriggerStunEnd;
            
            _animationEventCatcher.OnDeathAnimEnd += () => gameObject.SetActive(false);
            _myHp.OnDeath += () => _animator.SetTrigger(Death);
            _myHp.OnAnyDamageReceived += dmg => StartHitAnimation();
        }

        private void OnDisable()
        {
            _animationEventCatcher.OnAttackEnd -= SetTriggerAttackEnd;
            _animationEventCatcher.OnHitAnimEnd -= SetTriggerHitEnd;
            _animationEventCatcher.OnStunAnimEnd -= SetTriggerStunEnd;
        }
        
        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }

        public void UpdateDamage(float currentDamage, float currentCooldownTime)
        {
            _textDamage.text = $"{currentDamage}\n---\n{currentCooldownTime}";
        }
        
        public void StartAttackAnimation()
        {
            _animator.SetTrigger(Attack);
        }
        
        public void StartHitAnimation()
        {
            _animator.SetTrigger(Hit);
        }
        
        public void StartStunAnimation()
        {
            _animator.SetTrigger(Stun);
        }

        public void UpdateStunCircle(float currentStunProgress)
        {
            _stunSprite.fillAmount = currentStunProgress;
        }
        
        public void SetDialogueEndTrigger()
        {
            _animator.SetTrigger(DialogueEnd);
        }
    }
}
