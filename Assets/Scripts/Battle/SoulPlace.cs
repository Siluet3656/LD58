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
        private SoulType _skillType;
        private Image _spriteRenderer;

        private void Awake()
        {
            G.SoulPlaces.Add(this);
        }

        private void Start()
        {
            _mainCamera = Camera.main;

            _spriteRenderer = GetComponent<Image>();

            _skillType = SoulType.None;
        }

        public int GetSoulCost()
        {
            if (_skillType != SoulType.None)
                return SoulDataCms.Instance.GetSpellConfig(_skillType).cost;

            return -1;
        }

        public void PlaceSpell(SoulType skillType)
        {
            if (G.SoulChecker.IsSoulPlacingBlocked) return;
            
            _skillType = skillType;

            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(_skillType).icon;
        }

        public void RemoveSpell()
        {
            _skillType = SoulType.None;
            
            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(SoulType.None).icon;
        }
    }
}