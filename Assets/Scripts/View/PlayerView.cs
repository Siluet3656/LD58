using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Image _swingBar;
        [SerializeField] private Image _energyBar;
        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }
        
        public void UpdateEnergyBar(float currentEnergy)
        {
            _energyBar.fillAmount = currentEnergy;
        }
    }
}
