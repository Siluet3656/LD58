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
        [SerializeField] private SoulData _exiled;
        [SerializeField] private SoulData _knight;
        [SerializeField] private SoulData _merchant;
        [SerializeField] private SoulData _follower;
        [SerializeField] private SoulData _berserk;
        [SerializeField] private SoulData _leader;
        [SerializeField] private SoulData _soldier;
        [SerializeField] private SoulData _suspectDefiled;
        [SerializeField] private SoulData _suspectPure;
        [SerializeField] private SoulData _pureSoul;
        [SerializeField] private SoulData _scientist;
        [SerializeField] private SoulData _artificialSoul;
        
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
                { SoulType.Exiled, _exiled },
                { SoulType.Knight, _knight },
                { SoulType.Merchant, _merchant },
                { SoulType.Follower, _follower },
                { SoulType.Berserk, _berserk },
                { SoulType.Leader, _leader },
                { SoulType.Soldier, _soldier },
                { SoulType.SuspiciouslyDefiledSoul, _suspectDefiled },
                { SoulType.SuspiciouslyPureSoul, _suspectPure },
                { SoulType.SoulOfScientist, _scientist },
                { SoulType.PureSoul, _pureSoul },
                { SoulType.ArtificialSoul, _artificialSoul }
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