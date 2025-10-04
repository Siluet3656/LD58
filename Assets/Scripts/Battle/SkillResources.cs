using System.Collections;
using UnityEngine;
using View;

namespace Battle
{
    [RequireComponent(typeof(PlayerView))]
    public class SkillResources : MonoBehaviour
    {
        [SerializeField] private int _maxEnergy = 100;
        [SerializeField] private int _energyRestoredPerRate = 20;
        [SerializeField] private float _energyRestoreRate = 1f;

        private PlayerView _playerView;
        
        private int _currentEnergy;
        private bool _isReadyRestore = true;

        private void Awake()
        {
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
                    StartCoroutine(RestoreEnergy(_energyRestoreRate));
                }
            }
            
            UpdateUI();
        }

        private void GainEnergy(int amount)
        {
            _currentEnergy = Mathf.Min(_maxEnergy, _currentEnergy + amount);
        }

        private void UpdateUI()
        {
            _playerView.UpdateEnergyBar((float)_currentEnergy / _maxEnergy);
        }
        
        private IEnumerator RestoreEnergy(float cooldown)
        {
            _isReadyRestore = false;
            float elapsedTime = 0f;
            //_currentSwipeProgress = 0f;

            while (elapsedTime < cooldown)
            {
                elapsedTime += Time.deltaTime;
                //_currentSwipeProgress = Mathf.Clamp01(elapsedTime / cooldown);
               // _playerView.UpdateAttackSwingBar(_currentSwipeProgress);
                yield return null;
            }

            _isReadyRestore = true;
            //_currentSwipeProgress = 1f;
            //_playerView.UpdateAttackSwingBar(_currentSwipeProgress);
        }
        
        public bool HasEnoughResources(int energyCost)
        {
            return _currentEnergy >= energyCost;
        }

        public void ConsumeResources(int energyCost)
        {
            _currentEnergy -= (int)energyCost;
        }
    }
}