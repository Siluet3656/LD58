using UnityEngine;
using UnityEngine.UI;
using Data;

namespace Battle
{
    public class SoulPlace : MonoBehaviour
    {
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private int _id;
        
        private SkillArrowToTarget _arrow;
        private Camera _mainCamera;
        private SoulType _soulType;
        private Image _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<Image>();
        }

        private void Start()
        {
            if (G.SoulPlaces.ContainsKey(_id))
            {
                G.SoulPlaces.TryGetValue(_id, out _soulType);
                _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(_soulType).icon;
            }
            else
            {
                G.SoulPlaces.Add(_id, _soulType);
            }
        }
        
        public SoulType SoulType => _soulType;

        public void PlaceSpell(SoulType skillType)
        {
            if (G.SoulChecker.IsSoulPlacingBlocked) return;
            
            _soulType = skillType;
            
            G.SoulPlaces[_id] = skillType;

            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(_soulType).icon;
        }

        public void RemoveSpell()
        {
            _soulType = SoulType.None;
            
            G.SoulPlaces[_id] = SoulType.None;
            
            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(SoulType.None).icon;
        }
    }
}