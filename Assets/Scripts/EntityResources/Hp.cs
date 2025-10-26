using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;
using View;

namespace EntityResources
{
    [RequireComponent(typeof(HpView))]
    public class Hp : MonoBehaviour
    {
        [Header("Base Stats")]
        [SerializeField, Min(1)] private int _defaultMaxHealth;

        [SerializeField] private GameObject _floatingTextPrefab;
        [SerializeField] private GameObject _damageParticles;
        
        [SerializeField] private GameObject _soul;
        
        private RandomSoundPlayer _randomSoundPlayer;
        
        private float _maxHealth;
        private float _currentHealth;
        private float _additionalHealth;
        private int _shieldStacks;
        private bool _isInvulnerable;
        private int _knightSouls;
        
        private void Awake()
        {
            if (CompareTag("Player"))
            {
                G.PlayerHp = this;
                _knightSouls = 0;
            }
            else
            {
                _randomSoundPlayer = GetComponent<RandomSoundPlayer>();
            }
        }

        private void Start()
        {
            _maxHealth = _defaultMaxHealth;
            InitializeHealth();
        }
        
        private void ShowDialog(string message)
        {
            GameObject ft = Instantiate(_floatingTextPrefab, transform.position, Quaternion.identity);
            ft.GetComponent<FloatingText>().SetText(message);
        }

        private void TakeDamage(float damage)
        {
            if (_isInvulnerable) return;
            
            ApplyDamageToHealth(damage);
            
            Vector3 randPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y,transform.position.z);
            
            DamagePopup.Instance.AddText((int)damage, randPosition);
        }

        private void ApplyDamageToHealth(float damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            OnHealthChanged?.Invoke(_currentHealth);
            StartCoroutine(ParticlesAndFade());
            
            if (_currentHealth <= _maxHealth / 2)
            {
                int rand = Random.Range(0, 15);

                if (_currentHealth <= 1) rand = 7;
                
                
                
                if (CompareTag("Player"))
                {
                    switch (rand)
                    {
                        case 7:
                            ShowDialog("My experiments are at risk");
                            break;
                        case 14:
                            ShowDialog("Is this really the end?");
                            break;
                    }
                    
                }
                else
                { 
                    _randomSoundPlayer.PlayRandomSound();
                        
                    switch (rand)
                    {
                        case 0:
                            ShowDialog("Help me! Anyone?!");
                            break;
                        case 4:
                            ShowDialog("Not today!");
                            break;
                        case 6:
                            ShowDialog("I need a break...");
                            break;
                        case 7:
                            ShowDialog("Too hard!");
                            break;
                        case 13:
                            ShowDialog("You can’t stop me!");
                            break;
                    }
                }
            }
            
            if (_currentHealth <= 0)
                Die();
        }

        private IEnumerator ParticlesAndFade()
        {
            _damageParticles.SetActive(true);
            _damageParticles.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(1f);
            
            _damageParticles.SetActive(false);
        }

        private void Die()
        {
            OnDeath?.Invoke();
            
            GetInvulnerable();

            if (CompareTag("Enemy"))
            {
                WavyMoveToTarget soul = Instantiate(_soul, transform.position, Quaternion.identity).GetComponent<WavyMoveToTarget>();
                soul.target = G.Player.transform;
            }
        }
        
        public event Action<float> OnHealthChanged;
        public event Action OnDeath;
        public event Action<float> OnAnyDamageReceived; 

        public bool IsInvulnerable => _isInvulnerable;
        public float DefaultMaxHealth => _defaultMaxHealth;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth; 
        
        public void InitializeHealth()
        {
            _currentHealth = Mathf.Max(_maxHealth, 1);
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void SetMaxHealth(int health)
        {
            _maxHealth = health;
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void TryToTakeDamage(float damage, bool isDamageAdditional)
        {
            if (_isInvulnerable) return;
            
            if (isDamageAdditional == false) OnAnyDamageReceived?.Invoke(damage);
                
            TakeDamage(damage); //Debug.Log($"Damage taken by {gameObject}: {damage}");
        }

        public void GetInvulnerable()
        {
            _isInvulnerable = true;
        }
        
        public void GetVulnerable()
        {
            _isInvulnerable = false;
        }

        public void Heal(float healAmount)
        {
            if (healAmount <= 0) return;
            
            float resultHeal = Mathf.Min(_currentHealth + healAmount, _defaultMaxHealth); //Debug.Log($"Heal taken by {gameObject}: {resultHeal - _currentHealth}");
            _currentHealth = resultHeal;
            OnHealthChanged?.Invoke(_currentHealth);
            
            Vector3 randPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y,transform.position.z);
            
            DamagePopup.Instance.AddText(healAmount.ToString(CultureInfo.InvariantCulture), randPosition, Color.green);
        }

        public void RestoreHpForKnightSouls()
        {
            Heal(_knightSouls * 2);
        }

        public void SetKnightSouls(int knightSouls)
        {
            if (knightSouls < 0) return;
            
            _knightSouls = knightSouls;
        }
    }
}