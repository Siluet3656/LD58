using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _energyBar;
        [SerializeField] private Text _attackText;
        [SerializeField] private Text _energyText;

        private void Awake()
        {
            G.PlayerView = this;
        }

        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }
        
        public void UpdateEnergyBar(float currentEnergy)
        {
            _energyBar.fillAmount = currentEnergy;
            _energyText.text = $"{(int)(currentEnergy * 100)}/100";
        }

        public void UpdateAttackText(int currentAttack)
        {
            _attackText.text = $"{currentAttack}";
        }
    }
}
