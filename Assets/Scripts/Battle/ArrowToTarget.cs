using UnityEngine;

namespace Battle
{
    public class ArrowToTarget : MonoBehaviour
    {
        [SerializeField] private float _spriteWorldWidth = 1f;

        private Transform _source;
        private Transform _target;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_source is null || _target is null)
            {
                _spriteRenderer.enabled = false;
                return;
            }

            _spriteRenderer.enabled = true;
            
            Vector3 startPos = _source.position;
            Vector3 endPos = _target.position;
            Vector3 midPos = (startPos + endPos) / 2f;
            transform.position = midPos;
            
            Vector3 dir = endPos - startPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            float distance = dir.magnitude;
            transform.localScale = new Vector3(distance / _spriteWorldWidth, 1, 1);

        }

        public void SetupArrow(Transform source, Transform target)
        {
            if (source is null || target is null) return;
            if (source  == _target) return;
            
            _source = source;
            _target = target;
        }
    }
}