using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class AbilityTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _titleKey;
        [SerializeField] private string _abilityDescriptionKey;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.ShowTooltip(LocalizationManager.Instance.Get(_titleKey), LocalizationManager.Instance.Get(_abilityDescriptionKey));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}