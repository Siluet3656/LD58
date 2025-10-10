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
        private int _myKey;

        private void Start()
        {
            _spriteRenderer = GetComponent<Image>();
            
            if (G.SoulPlaces.ContainsKey(_id))
            {
                G.SoulPlaces.TryGetValue(_id, out _soulType);
                _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(_soulType).icon;
            }
            else
            {
                _myKey = _id;
                G.SoulPlaces.Add(_myKey, _soulType);
            }

            if (G.SoulPlaces.TryGetValue(_myKey, out var value))
            {
                _soulType = value;
            }
        }
        
        public SoulType SoulType => _soulType;

        public void PlaceSpell(SoulType skillType)
        {
            if (G.SoulChecker.IsSoulPlacingBlocked) return;
            
            _soulType = skillType;
            
            G.SoulPlaces.Remove(_myKey);
            G.SoulPlaces.Add(_myKey, _soulType);

            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(_soulType).icon;
        }

        public void RemoveSpell()
        {
            G.SoulPlaces.Remove(_myKey);
            
            _soulType = SoulType.None;
            
            _spriteRenderer.sprite = SoulDataCms.Instance.GetSpellConfig(SoulType.None).icon;
        }
    }
}