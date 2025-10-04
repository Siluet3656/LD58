using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class AbilityDataCms : MonoBehaviour
    {
        #region Singleton
        
        public static AbilityDataCms Instance { get; private set; }

        #endregion
        
        [SerializeField] private AbilityData _strike;
        
        
        private Dictionary<SkillType, AbilityData> _spellValues;
        
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
            _spellValues = new Dictionary<SkillType, AbilityData>
            {
                { SkillType.Strike, _strike },
            };
        }
        
        public AbilityData GetSpellConfig(SkillType spellName)
        {
            if (_spellValues.TryGetValue(spellName, out AbilityData config))
            {
                return config;
            }
    
            return null;
        }
    }
}