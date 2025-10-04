using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Prepare
{
    public class SpellButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [FormerlySerializedAs("_type")] [SerializeField] private SkillType _name;
        private AbilityDrag _hand;

        private void Start()
        {
            _hand = FindObjectsOfType<AbilityDrag>()[0];
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_name != SkillType.None)
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
