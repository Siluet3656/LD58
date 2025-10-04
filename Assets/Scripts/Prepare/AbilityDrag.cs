using Battle;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Prepare
{
    public class AbilityDrag : MonoBehaviour
    {
        private GameObject _corner;
        private Image _handIcon;
        private SkillType _draggingSpell;
        private Vector3 _point;
        private bool _isDragging;
        
        private readonly Vector2 _offset = new Vector2(0.5f,-0.5f); // Костыль

        private void Awake()
        {
            G.AbilityDrag = this;
            
            _corner = transform.GetChild(0).gameObject;
            _handIcon = GetComponent<Image>();
            
            _isDragging = false;
        }

        private void Update()
        {
            if (_isDragging)
            {
                transform.position = _point;
            }
        }
       
        private void DropSpell()
        {
            Cursor.visible = true;
            _handIcon.color = new Color(1,1,1,0);
            _corner.SetActive(false);
            _isDragging = false;
        }
        
        public void TakeSpell(SkillType spellName)
        {
            Cursor.visible = false;
            _handIcon.color = new Color(1,1,1,1);
            _corner.SetActive(true);
            _isDragging = true;
            
            AbilityData config = AbilityDataCms.Instance.GetSpellConfig(spellName);

            if (config == null)
            {
                DropSpell();
                return;
            }
            
            _draggingSpell = spellName;
            _handIcon.sprite = config.icon;
        }
        
        public void TryToDropASpell(RaycastHit2D hit)
        { 
            if (_isDragging)
            {
                AbilityButton spellBarButton = null;
            
                if (hit.collider == null) { DropSpell(); return; }

                spellBarButton = hit.collider.gameObject.GetComponent<AbilityButton>();  
                
                if (spellBarButton != null)
                {
                    spellBarButton.PlaceSpell(_draggingSpell);
                }
                
                DropSpell();
            }
        }
        
        public void PlaceSpell(Image place)
        {
            place.sprite = GetComponent<Image>().sprite;
        }
        
        public bool GetIsDragging()
        {
            return _isDragging;
        }

        public SkillType GetSpellType()
        {
            return _draggingSpell;
        }

        public bool CheckDraggingStatus()
        {
            return _isDragging;
        }

        public void SetPoint(Vector2 point)
        {
            _point = point;
            _point.x += _offset.x;
            _point.y += _offset.y;
        }
    }
}
