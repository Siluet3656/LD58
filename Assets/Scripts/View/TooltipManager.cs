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
            Instance._tooltip.SetText(header, content);
            Instance._tooltip.gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            Instance._tooltip.gameObject.SetActive(false);
        }

        public void SetMousePosition(Vector2 mousePosition)
        {
            Instance._tooltip.SetMousePosition(mousePosition);
        }
    }
}