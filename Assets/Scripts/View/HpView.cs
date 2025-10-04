using System;
using UnityEngine;
using UnityEngine.UI;
using EntityResources;

namespace View
{
    public class HpView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image _healthBar;
        [SerializeField] private Text _healthText;

        private Hp _hp;

        private void Awake()
        {
            _hp = GetComponent<Hp>();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            if (_hp == null) return;
            
            _hp.OnHealthChanged += UpdateHealthBar;
        }

        private void UnsubscribeFromEvents()
        {
            if (_hp == null) return;
            
            _hp.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar(float currentHealth)
        {
            if (_hp == null || _healthBar == null) return;
            
            _healthBar.fillAmount = currentHealth / _hp.MaxHealth;
            _healthText.text = $"{currentHealth}/{_hp.MaxHealth}";
        }
    }
}