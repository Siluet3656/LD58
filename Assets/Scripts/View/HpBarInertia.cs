using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HpBarInertia : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _phantomBar;

        [Header("Animation Settings")]
        [SerializeField] private float _delay = 0.25f;
        [SerializeField] private float _lerpSpeed = 3f;

        private float _targetFill;
        private float _delayTimer;

        public void SetHealth(float normalizedValue)
        {
            normalizedValue = Mathf.Clamp01(normalizedValue);
            
            _targetFill = normalizedValue;
            
            if (_phantomBar.fillAmount > _targetFill)
                _delayTimer = 0f;
            else
                _phantomBar.fillAmount = _targetFill;
        }

        private void Start()
        {
            _targetFill = _phantomBar.fillAmount;
        }

        private void Update()
        {
            if (_phantomBar.fillAmount > _targetFill)
            {
                _delayTimer += Time.deltaTime;

                if (_delayTimer >= _delay)
                {
                    _phantomBar.fillAmount = Mathf.Lerp(
                        _phantomBar.fillAmount,
                        _targetFill,
                        Time.deltaTime * _lerpSpeed
                    );
                }
            }
        }
    }
}