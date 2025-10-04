using Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{
    public class AbilityButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _arrowPrefab;
        
        private SkillArrowToTarget _arrow;
        private Camera _mainCamera;
        private SkillType _skillType;
        private Image _spriteRenderer;

        void Start()
        {
            _mainCamera = Camera.main;

            _spriteRenderer = GetComponent<Image>();

            _skillType = SkillType.None;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            GameObject arrowObj = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, transform.parent);
            _arrow = arrowObj.GetComponent<SkillArrowToTarget>();

            _arrow.SetSource(transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            if (_skillType == SkillType.None) return;
            
            if (_arrow == null) return;
            
            Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(eventData.position);
            mouseWorld.z = 0;
            
            _arrow.SetTargetPosition(mouseWorld);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_arrow == null)return;
            
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(eventData.position), Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out Enemy target))
            {
                target.ApplyAbility(_skillType);
            }
            else
            {
                //Debug.Log("Canceled!");
            }

            Destroy(_arrow.gameObject);
        }

        public void PlaceSpell(SkillType skillType)
        {
            _skillType = skillType;

            _spriteRenderer.sprite = AbilityDataCms.Instance.GetSpellConfig(skillType).icon;
        }
    }
}