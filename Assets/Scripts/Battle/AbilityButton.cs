using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle
{
    public class AbilityButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _arrowPrefab;
        
        private SkillArrowToTarget _arrow;
        private Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameObject arrowObj = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, transform.parent);
            _arrow = arrowObj.GetComponent<SkillArrowToTarget>();

            _arrow.SetSource(transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_arrow == null)
                return;
            
            Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(eventData.position);
            mouseWorld.z = 0;
            
            _arrow.SetTargetPosition(mouseWorld);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_arrow == null)
                return;
            
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(eventData.position), Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out Enemy target))
            {
                target.ApplyAbility();
            }
            else
            {
                //Debug.Log("Canceled!");
            }

            Destroy(_arrow.gameObject);
        }
    }
}