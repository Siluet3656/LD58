using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class ColorCyclerText : MonoBehaviour
    {
        [SerializeField] private Color _color1 = Color.red;
        [SerializeField] private Color _color2 = Color.blue;
        [SerializeField] private float _duration = 2.0f;

        private Text _text;
        private TMP_Text _tmpText;
        private float _timeElapsed;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _tmpText = GetComponent<TMP_Text>();
            _timeElapsed = 0f;
            if (_text == null && _tmpText == null)
            {
                Debug.LogError("Text component not found!");
                enabled = false;
            }
        }

        private void Update()
        {
            float t = Mathf.PingPong(_timeElapsed / _duration, 1f);

            if (_text != null)
            {
                _text.color = Color.Lerp(_color1, _color2, t);
            }
            else if (_tmpText != null)
            {
                _tmpText.color = Color.Lerp(_color1, _color2, t);
            }
            
            _timeElapsed += Time.deltaTime;
        }
    }
}