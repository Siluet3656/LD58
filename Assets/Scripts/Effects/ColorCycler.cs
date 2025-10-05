using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class ColorCycler : MonoBehaviour
    {
        [SerializeField] private Color _color1 = Color.red;
        [SerializeField] private Color _color2 = Color.blue;
        [SerializeField] private float _duration = 2.0f;

        private SpriteRenderer _spriteRenderer;
        private float _timeElapsed;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timeElapsed = 0f;
            if (_spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component not found!");
                enabled = false;
            }
        }

        private void Update()
        {
            float t = Mathf.PingPong(_timeElapsed / _duration, 1f);
            _spriteRenderer.color = Color.Lerp(_color1, _color2, t);
            _timeElapsed += Time.deltaTime;
        }
    }
}