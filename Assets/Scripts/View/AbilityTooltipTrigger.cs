using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class AbilityTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _titleKey;
        [SerializeField] private string _abilityDescriptionKey;
        
        private float _delay = 0.25f;
        private bool _isTooltipScheduled = false;
        private const string SHOW_TOOLTIP_METHOD = "ShowTooltipDelayed";

        private void OnDisable()
        {
            CancelInvoke(SHOW_TOOLTIP_METHOD);
            
            if (_isTooltipScheduled || TooltipManager.Instance != null)
            {
                TooltipManager.Instance?.HideTooltip();
            }
        }
        
        private void ShowTooltipDelayed()
        {
            if (gameObject.activeInHierarchy)
            {
                TooltipManager.Instance.ShowTooltip(
                    LocalizationManager.Instance.Get(_titleKey),
                    LocalizationManager.Instance.Get(_abilityDescriptionKey)
                );
                _isTooltipScheduled = false;
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            CancelInvoke(SHOW_TOOLTIP_METHOD);
            
            Invoke(SHOW_TOOLTIP_METHOD, _delay);
            _isTooltipScheduled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CancelInvoke(SHOW_TOOLTIP_METHOD);
            _isTooltipScheduled = false;

            TooltipManager.Instance.HideTooltip();
        }
    }
}