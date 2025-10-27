using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using View;

namespace Battle
{
    [RequireComponent(typeof(PlayerView))]
    public class SkillResources : MonoBehaviour
    {
        [SerializeField] private int _defaultMaxEnergy = 100;
        [SerializeField] private int _defaultEnergyRestoredPerRate = 20;
        [SerializeField] private float _energyRestoreRate = 1f;
        [SerializeField] private Image _energyBar;

        private PlayerView _playerView;
        
        private int _currentEnergy;
        private bool _isReadyRestore = true;
        private float _currentSwipeProgress;

        private int _adjustedEnergyRestoredPerRate;
        private int _adjustedMaxEnergy;
        
        private int _scienceSouls;

        private void Awake()
        {
            G.SkillResources = this;
            
            _playerView = GetComponent<PlayerView>();

            _adjustedMaxEnergy = _defaultMaxEnergy;
            _currentEnergy = _adjustedMaxEnergy;
            UpdateUI();
            
            _scienceSouls = 0;
        }

        private void Update()
        {
            if (IsEnergyRegenerate)
            {
                if (_currentEnergy != _adjustedMaxEnergy)
                {
                    if (_isReadyRestore)
                    {
                        ChangeEnergy(_adjustedEnergyRestoredPerRate);
                        StartCoroutine(RestoreEnergy(_energyRestoreRate));
                    }
                }
            }
            
            UpdateUI();
        }

        private void ChangeEnergy(int amount)
        {
            if (amount == 0) return;
            
            _currentEnergy = Mathf.Min(_adjustedMaxEnergy, _currentEnergy + amount);
            _currentEnergy = Mathf.Max(0, _currentEnergy);

            if (amount > 0)
            {
                DamagePopup.Instance.AddText($"+{amount}", new Vector3(_energyBar.transform.position.x + Random.Range(-2f, 2f),
                        _energyBar.transform.position.y,
                        _energyBar.transform.position.z), 
                    new Color(59, 35, 99));
            }
            else
            {
                G.PlayerHp.Heal(_scienceSouls * 1f * (amount * -1)/ 10);
                
                DamagePopup.Instance.AddText($"{amount}", new Vector3(_energyBar.transform.position.x + Random.Range(-2f, 2f),
                        _energyBar.transform.position.y,
                        _energyBar.transform.position.z), 
                    new Color(59, 35, 99));
            }
        }

        private void UpdateUI()
        {
            _playerView.UpdateEnergyBar(_currentEnergy, _adjustedMaxEnergy);
        }
        
        private IEnumerator RestoreEnergy(float cooldown)
        {
            _isReadyRestore = false;
            float elapsedTime = 0f;
            _currentSwipeProgress = 0f;

            while (elapsedTime < cooldown)
            {
                elapsedTime += Time.deltaTime;
                _currentSwipeProgress = Mathf.Clamp01(elapsedTime / cooldown);
               UpdateBar(_currentSwipeProgress);
                yield return null;
            }

            _isReadyRestore = true;
            _currentSwipeProgress = 1f;
            UpdateBar(_currentSwipeProgress);
        }

        private void UpdateBar(float progress)
        {
            _energyBar.fillAmount = progress / 1f ;
        }

        public bool IsEnergyRegenerate;
        public int DefaultMaxEnergy => _defaultMaxEnergy;
        public int DefaultEnergyRestoredPerRate => _defaultEnergyRestoredPerRate;
        
        public bool HasEnoughResources(int energyCost)
        {
            return _currentEnergy >= energyCost;
        }

        public void ConsumeResources(int energyCost)
        {
            if (energyCost <= 0) return;

            ChangeEnergy(energyCost * -1);
        }

        public void RestoreResources(int energy)
        {
            ChangeEnergy(energy);
        }

        public void AdjustResources(int additionalAmount)
        {
            if (additionalAmount < 0) return;

            _adjustedEnergyRestoredPerRate = _defaultEnergyRestoredPerRate + additionalAmount;
        }

        public void AdjustMaxEnergy(int amount)
        {
            if (amount <= 0)
            {
                _adjustedMaxEnergy = _defaultMaxEnergy;
                _currentEnergy = _adjustedMaxEnergy;
                return;
            }
            
            _adjustedMaxEnergy = amount;
            _currentEnergy = _adjustedMaxEnergy;
        }

        public void SetScienceSouls(int scienceSouls)
        {
            _scienceSouls = scienceSouls;
        }
    }
}