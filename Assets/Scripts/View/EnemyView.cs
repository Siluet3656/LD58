using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Image _swingBar;
        public void UpdateAttackSwingBar(float currentSwipeProgress)
        {
            _swingBar.fillAmount = currentSwipeProgress;
        }
    }
}
