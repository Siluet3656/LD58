using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class ColorCycler : MonoBehaviour
    {
        [SerializeField] private Color _color1 = Color.red;
        [SerializeField] private Color _color2 = Color.blue;
        [SerializeField] private float _duration = 2.0f;

        private Text _text;
        private float _timeElapsed;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _timeElapsed = 0f;
            if (_text == null)
            {
                Debug.LogError("Text component not found!");
                enabled = false;
            }
        }

        private void Update()
        {
            float t = Mathf.PingPong(_timeElapsed / _duration, 1f);
            _text.color = Color.Lerp(_color1, _color2, t);
            _timeElapsed += Time.deltaTime;
        }
    }
}