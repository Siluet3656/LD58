using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class AbilityTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string _title;
        [TextArea(2, 5)]
        [SerializeField] private string _abilityDescription;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.ShowTooltip(_title, _abilityDescription);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}