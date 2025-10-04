using System;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{
    public class SoulPlace : MonoBehaviour
    {
        [SerializeField] private GameObject _arrowPrefab;
        
        private SkillArrowToTarget _arrow;
        private Camera _mainCamera;
        private SoulType _soulType;
        private Image _spriteRenderer;

        private void Awake()
        {
            G.SoulPlaces.Add(this);
        }

        private void Start()
        {
            _mainCamera = Camera.main;

            _spriteRenderer = GetComponent<Image>();

            _soulType = SoulType.None;
        }
        
        public SoulType SoulType => _soulType;

        public int GetSoulCost()
        {
            if (_soulType != SoulType.None)
                return SoulDataCms.Instance.GetSpellConfig(_soulType).cost;

            return -1;
        }

        public void PlaceSpell(SoulType skillType)
        {
            if (G.SoulChecker.IsSoulPlacingBlocked) return;
            
            _soulType = skillType;

            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(_soulType).icon;
        }

        public void RemoveSpell()
        {
            _soulType = SoulType.None;
            
            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(SoulType.None).icon;
        }
    }
}