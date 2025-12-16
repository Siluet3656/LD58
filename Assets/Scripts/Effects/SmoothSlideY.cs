using UnityEngine;
using View;

namespace Effects
{
    public class SmoothSlideY : MonoBehaviour
    {
        [Header("Positions")]
        [SerializeField] private float _shownY = 0f; 
        [SerializeField] private float _hiddenY = 500f;
        
        [Header("Animation")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private bool _startHidden = true;

        private RectTransform _rectTransform;
        private bool _isShown;
        private float _targetY;

        private void Awake()
        {
            G.SmoothSlideY = this;
            
            _rectTransform = GetComponent<RectTransform>();

            if (_startHidden)
            {
                var pos = _rectTransform.anchoredPosition;
                pos.y = _hiddenY;
                _rectTransform.anchoredPosition = pos;
                _isShown = false;
            }

            _targetY = _rectTransform.anchoredPosition.y;
        }

        private void Update()
        {
            Vector2 pos = _rectTransform.anchoredPosition;
            pos.y = Mathf.Lerp(pos.y, _targetY, Time.deltaTime * _speed);
            _rectTransform.anchoredPosition = pos;
        }

        public void Toggle()
        {
            _isShown = !_isShown;
            _targetY = _isShown ? _shownY : _hiddenY;
        }

        public void Show()
        {
            _isShown = true;
            _targetY = _shownY;

            foreach (var enemy in G.Enemies)
            {
                enemy.GetComponent<EnemyView>().SetDialogueEndTrigger();
            }
        }
    
        public void Hide()
        {
            _isShown = false;
            _targetY = _hiddenY;
        }
    }
}