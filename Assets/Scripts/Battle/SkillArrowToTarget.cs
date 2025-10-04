using UnityEngine;

namespace Battle
{
    public class SkillArrowToTarget : MonoBehaviour
    {
        private Transform _source;
        private Vector3? _targetPosition;
        private SpriteRenderer _spriteRenderer;
        private float _spriteWorldWidth;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteWorldWidth = _spriteRenderer.bounds.size.x;
        }

        void Update()
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            if (_source == null || !_targetPosition.HasValue)
            {
                _spriteRenderer.enabled = false;
                return;
            }

            _spriteRenderer.enabled = true;

            Vector3 startPos = _source.position;
            Vector3 endPos = _targetPosition.Value;
            Vector3 midPos = (startPos + endPos) / 2f;
            transform.position = midPos;

            Vector3 dir = endPos - startPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            float distance = dir.magnitude;
            transform.localScale = new Vector3(distance / _spriteWorldWidth, 1, 1);
        }

        public void SetSource(Transform source) => _source = source;
        public void SetTargetPosition(Vector3 pos) => _targetPosition = pos;
    }
}