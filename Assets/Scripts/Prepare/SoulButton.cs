using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Prepare
{
    public class SoulButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [FormerlySerializedAs("_type")] [SerializeField] private SoulType _name;
        private AbilityDrag _hand;

        private void Start()
        {
            _hand = FindObjectsOfType<AbilityDrag>()[0];
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_name != SoulType.None)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    _hand.TakeSpell(_name);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        
        }

        public void OnDrag(PointerEventData eventData)
        {
        
        }
    }
}
