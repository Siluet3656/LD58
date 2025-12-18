using UnityEngine;

namespace View
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        
        [SerializeField] private Tooltip _tooltip;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowTooltip(string header, string content)
        {
            _tooltip.SetText(header, content);
            _tooltip.gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            _tooltip.gameObject.SetActive(false);
        }

        public void SetMousePosition(Vector2 mousePosition)
        {
            _tooltip.SetMousePosition(mousePosition);
        }
    }
}