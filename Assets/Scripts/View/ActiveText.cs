using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ActiveText
    {
        private readonly Text _uiText;
        private readonly float _maxTime;
        private readonly Vector3 _unitPosition;
        
        private float _timer;

        public Text UiText => _uiText;
        
        public ActiveText(float maxTime, Text uiText, Vector3 unitPosition)
        {
            _uiText = uiText;
            _maxTime = maxTime;
            _timer = maxTime;
            _unitPosition = unitPosition + Vector3.up;
        }
        
        public void MoveText(Camera camera)
        { 
            float delta = 1f - (_timer / _maxTime);
            Vector3 position = _unitPosition + new Vector3(0, delta, 0f);
            position = camera.WorldToScreenPoint(position);
            position.z = 0f;
            
            RectTransform canvasRectTransform = _uiText.canvas.GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                position,
                camera,
                out var localPoint
            );

            _uiText.rectTransform.localPosition = localPoint;
        }

        public bool DecreaseTimerAndCheckIt(float amount)
        {
            if (amount <= 0) return true;
            
            _timer -= amount;

            if (_timer <= 0f) return true;
            
            return false;
        }

        public void FadeOut()
        {
            Color color = UiText.color;
            color.a = _timer / _maxTime;
            UiText.color = color;
        }
    }
}
