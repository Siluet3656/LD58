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
            int knightsSouls = 0;
            int merchantSouls = 0;
            int followerSouls = 0;
            int berserkSouls = 0;
            int leaderSouls = 0;
            int soldierSouls = 0;
            int suspiciouslyDefiledSouls = 0;
            int suspiciouslyPureSouls = 0;
            int scientistSouls = 0;
            int pureSouls = 0;
            int artificialSouls = 0;

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
                    case SoulType.Knight:
                        knightsSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Merchant:
                        merchantSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Follower:
                        followerSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Berserk:
                        berserkSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Leader:
                        leaderSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.Soldier:
                        soldierSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.SuspiciouslyDefiledSoul:
                        suspiciouslyDefiledSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.SuspiciouslyPureSoul:
                        suspiciouslyPureSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.SoulOfScientist:
                        scientistSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.PureSoul:
                        pureSouls++;
                        _totalSouls++;
                        break;
                    case SoulType.ArtificialSoul:
                        artificialSouls++;
                        _totalSouls++;
                        break;
                }
            }

            int health = (int)(G.PlayerHp.DefaultMaxHealth - (2 * poorManSouls) +
                         leaderSouls * (5 * (G.Enemies.Count + 1))) * (1 + 1 * pureSouls)  ;
            G.PlayerHp.SetMaxHealth(health);
            G.PlayerHp.InitializeHealth();
            int attack = (int)(G.PlayerAttack.DefaultDamage + (1 * poorManSouls) + soldierSouls * (1 * (G.Enemies.Count + 1))) * (1 + 1 * pureSouls);
            G.PlayerAttack.AdjustDamage(attack);
            G.PlayerView.UpdateAttackText(attack, G.PlayerAttack.DefaultCooldownTime);
            
            G.PlayerAttack.SetUpEnergyRestorePerAttack(banditsSouls * 10 - suspiciouslyPureSouls * 10);
            
            int baseEnergy = G.SkillResources.DefaultEnergyRestoredPerRate;
            int bonusFromExiled = exiledSouls * 5;

            int total = baseEnergy + bonusFromExiled;

            if (artificialSouls > 0)
                total *= 2 * artificialSouls;
            int extra = total - baseEnergy;

            G.SkillResources.AdjustResources(extra);
            
            G.PlayerHp.SetKnightSouls(knightsSouls);

            foreach (Enemy enemy in BattleRuler.Instance.EnemiesOnScene)
            {
                enemy.SetMerchantSouls(merchantSouls);
                enemy.SetFollowerSouls(followerSouls);
            }

            G.PlayerHp.SetFollowerSouls(followerSouls);
            
            G.PlayerHp.SetBerserkSouls(berserkSouls);
            
            G.PlayerAttack.SetCooldownTime(G.PlayerAttack.DefaultCooldownTime - suspiciouslyPureSouls * 0.5f - suspiciouslyDefiledSouls * 0.5f);
            
            G.PlayerAttack.SetDefiledSouls(suspiciouslyDefiledSouls);
            
            G.SkillResources.SetScienceSouls(scientistSouls);

            if (pureSouls > 0)
            {
                G.SkillResources.IsEnergyRegenerate = false;
            }
            else
            {
                G.SkillResources.IsEnergyRegenerate = true;
            }
            
            G.SkillResources.AdjustMaxEnergy(G.SkillResources.DefaultMaxEnergy * artificialSouls * 2);
        }
        
        public bool IsSoulPlacingBlocked =>  _isSoulPlacingBlocked;
        public int TotalSouls => _totalSouls;
    }
}
