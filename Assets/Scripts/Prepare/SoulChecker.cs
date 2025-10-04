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
            int poorManSouls = 0;
            
            foreach (SoulPlace soulPlace in G.SoulPlaces)
            {
                switch (soulPlace.SoulType)
                {
                    case SoulType.PoorMan:
                        poorManSouls++;
                        break;
                }
            }
            
            G.PlayerHp.SetMaxHealth((int)G.PlayerHp.DefaultMaxHealth - (2 * poorManSouls));
            G.PlayerHp.InitializeHealth();
            
            int attack = (int)G.PlayerAttack.DefaultDamage + (1 * poorManSouls);
            G.PlayerAttack.AdjustDamage(attack);
            G.PlayerView.UpdateAttackText(attack);
        }
        
        public bool IsSoulPlacingBlocked =>  _isSoulPlacingBlocked;
    }
}
