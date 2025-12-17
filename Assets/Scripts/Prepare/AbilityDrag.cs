using System;
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
        private bool _isDraggingSoul;
        
        private SoulType _draggingSoul;
        
        private readonly Vector2 _offset = new Vector2(0.5f,-0.5f); // Костыль
        
        private RandomSoundPlayer _placeSound;
        private RandomSoundPlayer _dropSound;
        private RandomSoundPlayer _pickupSound;
        private RandomSoundPlayer _wrongSound;
        
        private void Awake()
        {
            G.AbilityDrag = this;
            
            _corner = transform.GetChild(0).gameObject;
            _handIcon = GetComponent<Image>();
            
            _isDragging = false;
            _isDraggingSoul = false;
        }

        private void Start()
        {
            SoundBank soundBank = G.SoundBank;
            _placeSound = soundBank.PLaceSound;
            _dropSound = soundBank.DropSound;
            _pickupSound = soundBank.PickupSound;
            _wrongSound = soundBank.WrongSound;
        }

        private void Update()
        {
            if (_isDragging)
            {
                transform.position = _point;
            }
            
            if (_isDraggingSoul)
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
            _isDraggingSoul = false;
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
        
        public void TakeSpell(SoulType soulName)
        {
            Cursor.visible = false;
            _handIcon.color = new Color(1,1,1,1);
            _corner.SetActive(true);
            _isDraggingSoul = true;
            
            SoulData config = SoulDataCms.Instance.GetSpellConfig(soulName);

            if (config == null)
            {
                DropSpell();
                return;
            }
            
            _draggingSoul = soulName;
            _handIcon.sprite = config.icon;
            
            _pickupSound.PlayRandomSound();
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
                    //spellBarButton.PlaceSpell(_draggingSpell);
                }
                
                DropSpell();
            }

            if (_isDraggingSoul)
            {
                SoulPlace SoulBarButton = null;

                if (hit.collider == null)
                {
                    DropSpell();
                    _dropSound.PlayRandomSound();
                    return;
                }

                if (G.SoulChecker.IsSoulPlacingBlocked)
                {
                    _wrongSound.PlayRandomSound();
                    return;
                }

                SoulBarButton = hit.collider.gameObject.GetComponent<SoulPlace>();  
                
                if (SoulBarButton != null)
                {
                    SoulBarButton.PlaceSpell(_draggingSoul);
                    _placeSound.PlayRandomSound();
                }
                
                DropSpell();
            }
        }

        public SoulType GetSoul()
        {
            return _draggingSoul;
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
        
        public bool CheckSoulDraggingStatus()
        {
            return _isDraggingSoul;
        }

        public void SetPoint(Vector2 point)
        {
            _point = point;
            _point.x += _offset.x;
            _point.y += _offset.y;
        }
    }
}
