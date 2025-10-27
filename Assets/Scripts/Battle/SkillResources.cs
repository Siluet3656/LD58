using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Battle
{
    [RequireComponent(typeof(PlayerView))]
    public class SkillResources : MonoBehaviour
    {
        [SerializeField] private int _maxEnergy = 100;
        [SerializeField] private int _energyRestoredPerRate = 20;
        [SerializeField] private float _energyRestoreRate = 1f;
        [SerializeField] private Image _energyBar;

        private PlayerView _playerView;
        
        private int _currentEnergy;
        private bool _isReadyRestore = true;
        private float _currentSwipeProgress;

        private int _adjustedEnergyRestoredPerRate;

        private void Awake()
        {
            G.SkillResources = this;
            
            _playerView = GetComponent<PlayerView>();

            _currentEnergy = _maxEnergy;
            UpdateUI();
        }

        private void Update()
        {
            if (_currentEnergy != _maxEnergy)
            {
                if (_isReadyRestore)
                {
                    ChangeEnergy(_adjustedEnergyRestoredPerRate);
                    StartCoroutine(RestoreEnergy(_energyRestoreRate));
                }
            }
            
            UpdateUI();
        }

        private void ChangeEnergy(int amount)
        {
            _currentEnergy = Mathf.Min(_maxEnergy, _currentEnergy + amount);
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
                DamagePopup.Instance.AddText($"{amount}", new Vector3(_energyBar.transform.position.x + Random.Range(-2f, 2f),
                        _energyBar.transform.position.y,
                        _energyBar.transform.position.z), 
                    new Color(59, 35, 99));
            }
        }

        private void UpdateUI()
        {
            _playerView.UpdateEnergyBar(_currentEnergy, _maxEnergy);
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
        
        public bool HasEnoughResources(int energyCost)
        {
            return _currentEnergy >= energyCost;
        }

        public void ConsumeResources(int energyCost)
        {
            if (energyCost <= 0) return;
            
            _currentEnergy -= energyCost;
        }

        public void RestoreResources(int energy)
        {
            ChangeEnergy(energy);
        }

        public void AdjustResources(int additionalAmount)
        {
            if (additionalAmount < 0) return;

            _adjustedEnergyRestoredPerRate = _energyRestoredPerRate + additionalAmount;
        }
    }
}