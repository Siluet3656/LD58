﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int AttackEnd = Animator.StringToHash("AttackEnd");
        private static readonly int Attack = Animator.StringToHash("Attack");
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _energyBar;
        [SerializeField] private Text _attackText;
        [SerializeField] private Text _energyText;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventCatcher _animationEventCatcher;

        private void Awake()
        {
            G.PlayerView = this;
        }

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
    }
}
