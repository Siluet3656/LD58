using UnityEngine;

namespace Effects
{
    public class ShadowFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void Update()
        {
            if (_target != null)
                transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z);
        }
    }
}
