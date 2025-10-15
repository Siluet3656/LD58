using UnityEngine;
using UnityEngine.UI;
using Battle;
using Data;

namespace Prepare
{
    public class SoulChecker : MonoBehaviour
    {
        [SerializeField] private Text _costText;
        [SerializeField] private int _maxCost;

        private bool _isSoulPlacingBlocked = false;
        private int _totalSouls;
        
        private void Awake()
        {
            G.SoulChecker = this;
        }

        private void OnDestroy()
        {
            G.SoulChecker = null;
            //G.SoulPlaces.Clear();
        }

        private void Update()
        {
            int totalCurrentCost = 0;
            
            foreach (var soulPlace in G.SoulPlaces)
            {
                if (SoulDataCms.Instance.GetSpellConfig(soulPlace.Value).cost > 0)
                    totalCurrentCost += SoulDataCms.Instance.GetSpellConfig(soulPlace.Value).cost;
            }

            if (totalCurrentCost + SoulDataCms.Instance.GetSpellConfig(G.AbilityDrag.GetSoul()).cost > _maxCost)
            {
                _isSoulPlacingBlocked = true;
            }
            else
            {
                _isSoulPlacingBlocked = false;
            }

            UpdatePlayerStatus();
            
            _costText.text = $"{totalCurrentCost} / {_maxCost}";
        }

        private void UpdatePlayerStatus()
        {
            if (BattleRuler.Instance.IsFighting) return;
            
            int poorManSouls = 0;
            int banditsSouls = 0;
            int exiledSouls = 0;

            _totalSouls = 0;
            
            foreach (var soulPlace in G.SoulPlaces)
            {
                switch (soulPlace.Value)
                {
                    case SoulType.PoorMan:
                        poorManSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Bandit:
                        banditsSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Exiled:
                        exiledSouls++;
                        _totalSouls++;
                        break;
                }
            }
            
            G.PlayerHp.SetMaxHealth((int)G.PlayerHp.DefaultMaxHealth - (2 * poorManSouls));
            G.PlayerHp.InitializeHealth();
            
            int attack = (int)G.PlayerAttack.DefaultDamage + (1 * poorManSouls);
            G.PlayerAttack.AdjustDamage(attack);
            G.PlayerView.UpdateAttackText(attack);
            
            G.PlayerAttack.SetUpEnergyRestorePerAttack(banditsSouls * 10);
            
            G.SkillResources.AdjustResources(exiledSouls * 5);
        }
        
        public bool IsSoulPlacingBlocked =>  _isSoulPlacingBlocked;
        public int TotalSouls => _totalSouls;
    }
}
