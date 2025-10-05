using TMPro;
using UnityEngine;

namespace View
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private  float _duration = 2f;
        [SerializeField] private  Vector3 _offset = new Vector3(0, 2f, 0);

        private float timer;

        void Start()
        {
            timer = _duration;
            transform.position += _offset;
        }

        void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
            
            transform.position += new Vector3(0, Time.deltaTime, 0);
        }

        public void SetText(string message)
        {
            _textMesh.text = message;
        }
    }
}