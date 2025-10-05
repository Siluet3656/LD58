using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Image _swingBar;
        [SerializeField] private Text _textDamage;
        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }

        public void UpdateDamage(float currentDamage)
        {
            _textDamage.text = $"{currentDamage}";
        }
    }
}
