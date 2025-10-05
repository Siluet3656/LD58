using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class SoulDataCms : MonoBehaviour
    {
        #region Singleton
        
        public static SoulDataCms Instance { get; private set; }

        #endregion

        [SerializeField] private SoulData _none;
        [SerializeField] private SoulData _poorMan;
        [SerializeField] private SoulData _bandit;
        
        
        private Dictionary<SoulType, SoulData> _spellValues;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            
            UpdateData();
        }

        public void UpdateData()
        {
            _spellValues = new Dictionary<SoulType, SoulData>
            {
                { SoulType.None, _none },
                { SoulType.PoorMan, _poorMan },
                { SoulType.Bandit, _bandit },
            };
        }
        
        public SoulData GetSpellConfig(SoulType soulName)
        {
            if (_spellValues.TryGetValue(soulName, out SoulData config))
            {
                return config;
            }
    
            return null;
        }
    }
}