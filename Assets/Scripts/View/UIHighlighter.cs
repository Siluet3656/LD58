using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class UIHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Graphic _targetGraphic;
        [SerializeField] private Color _highlightColor = Color.black;
        [SerializeField] private bool _useCustomNormalColor = false;
        [SerializeField] private Color _normalColor = Color.white;

        private Color _initialColor;

        private void Awake()
        {
            if (_targetGraphic == null)
                _targetGraphic = GetComponent<Graphic>();

            if (!_useCustomNormalColor && _targetGraphic != null)
                _initialColor = _targetGraphic.color;
            else
                _initialColor = _normalColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (G.AbilityDrag.GetIsDragging() == false) return;
            
            if (_targetGraphic != null)
                _targetGraphic.color = _highlightColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_targetGraphic != null)
                _targetGraphic.color = _initialColor;
        }
    }
}