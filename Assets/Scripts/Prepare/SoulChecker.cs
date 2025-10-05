using System;
using Battle;
using EntityResources;
using UnityEngine;
using UnityEngine.UI;

namespace Prepare
{
    public class SoulChecker : MonoBehaviour
    {
        [SerializeField] private Text _costText;
        [SerializeField] private int _maxCost;

        private bool _isSoulPlacingBlocked = false;
        
        private void Awake()
        {
            G.SoulChecker = this;
        }

        private void OnDestroy()
        {
            G.SoulChecker = null;
            G.SoulPlaces.Clear();
        }

        private void Update()
        {
            int sum = 0;
            foreach (SoulPlace soulPlace in G.SoulPlaces)
            {
                if (soulPlace.GetSoulCost() > 0)
                    sum += soulPlace.GetSoulCost();
            }

            if (sum >= _maxCost)
            {
                _isSoulPlacingBlocked = true;
            }
            else
            {
                _isSoulPlacingBlocked = false;
            }

            UpdatePlayerStatus();
            
            _costText.text = $"{sum} / {_maxCost}";
        }

        private void UpdatePlayerStatus()
        {
            if (BattleRuler.Instance.IsFighting) return;
            
            int poorManSouls = 0;
            int banditsSouls = 0;
            int exiledSouls = 0;
            
            foreach (SoulPlace soulPlace in G.SoulPlaces)
            {
                switch (soulPlace.SoulType)
                {
                    case SoulType.PoorMan:
                        poorManSouls++;
                        break;
                    case SoulType.Bandit:
                        banditsSouls++;
                        break;
                    case SoulType.Exiled:
                        exiledSouls++;
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
    }
}
