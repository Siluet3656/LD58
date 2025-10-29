using System;
using System.Collections;
using System.Globalization;
using Battle;
using Data;
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
        [SerializeField] private float _shieldDuration;
        [SerializeField] private GameObject _soul;
        
        private RandomSoundPlayer _randomSoundPlayer;
        
        private float _maxHealth;
        private float _currentHealth;
        private float _additionalHealth;
        private int _shieldStacks;
        private bool _isInvulnerable;
        private int _knightSouls;
        private int _followerSouls;
        private int _berserkSouls;
        private bool _isShielded;
        private int _healAfterHealthDrop;
        private bool _isRegenerating;
        private bool _isRegenerated;
        private float _regenerationTimer = 1f;
        private float _regenerationAmount = 3f;
        private bool _isLosingHealthAtStart;

        private void Awake()
        {
            _isRegenerating = false;
            _isLosingHealthAtStart = false;
            
            if (CompareTag("Player"))
            {
                G.PlayerHp = this;
                _knightSouls = 0;
                _followerSouls = 0;
                _berserkSouls = 0;
                _isShielded = false;
            }
            else
            {
                _randomSoundPlayer = GetComponent<RandomSoundPlayer>();
                _healAfterHealthDrop = 0;
                _isRegenerated = false;
            }
        }

        private void OnEnable()
        {
            BattleRuler.Instance.OnFighting += TryToLoseHealthAtStart;
        }

        private void OnDisable()
        {
            BattleRuler.Instance.OnFighting -= TryToLoseHealthAtStart;
        }
        
        private void TryToLoseHealthAtStart()
        {
            if (_isLosingHealthAtStart)
            {
                TryToTakeDamage(_maxHealth * 0.5f, false);
            }
        }

        private void Start()
        {
            _maxHealth = _defaultMaxHealth;
            InitializeHealth();
        }

        private void Update()
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            if (_followerSouls > 0)
            {
                if (_currentHealth <= _maxHealth * 0.4f)
                {
                    Heal(_maxHealth);
                    _followerSouls--;
                }
            }

            if (_berserkSouls > 0)
            {
                if (_currentHealth <= _maxHealth * 0.5f)
                {
                    G.PlayerAttack.SetUpDamageDoubling(true);
                }
                else
                {
                    G.PlayerAttack.SetUpDamageDoubling(false);
                }
            }

            if (_healAfterHealthDrop > 0)
            {
                if (_currentHealth <= _maxHealth * 0.4f)
                {
                    Heal(_maxHealth);
                    _healAfterHealthDrop--;
                }
            }

            if (_isRegenerating)
            {
                if (_isRegenerated == false)
                {
                    StartCoroutine(RegenerateRoutine());
                }
            }
        }

        private void ShowDialog(string message)
        {
            GameObject ft = Instantiate(_floatingTextPrefab, transform.position, Quaternion.identity);
            ft.GetComponent<FloatingText>().SetText(message);
        }

        private void TakeDamage(float damage)
        {
            if (_isInvulnerable)
            {
                Vector3 randPos = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y,transform.position.z);
                DamagePopup.Instance.AddText("Invulnerable!!", randPos, Color.white);
                return;
            }
            
            if (damage <= 0) return;
            
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
        
        private IEnumerator ShieldRoutine()
        {
            _isShielded = true;
            
            G.PlayerView.PlayShieldAnimation();
            GetInvulnerable();
            
            Vector3 randPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y,transform.position.z);
            DamagePopup.Instance.AddText("Invulnerable!!", randPosition, Color.cyan);
            
            float elapsed = 0f;
            while (elapsed < _shieldDuration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            GetVulnerable();
            
            randPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f),transform.position.y,transform.position.z);
            DamagePopup.Instance.AddText("Vulnerable!!", randPosition, Color.cyan);
            
            _isShielded = false;
        }

        private IEnumerator RegenerateRoutine()
        {
            _isRegenerated = true;
            
            float elapsed = 0f;
            while (elapsed < _regenerationTimer)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            Heal(_regenerationAmount);
            
            _isRegenerated = false;
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

        public void SetFollowerSouls(int followerSouls)
        {
            if (followerSouls < 0) return;
            
            _followerSouls = followerSouls;
        }

        public void SetBerserkSouls(int berserkSouls)
        {
            if (berserkSouls < 0) return;
            
            _berserkSouls = berserkSouls;
        }

        public void ApplyShield()
        {
            if (_isShielded) return;
            
            if (G.SkillResources.HasEnoughResources(AbilityDataCms.Instance.GetSpellConfig(SkillType.Shield).cost) == false) return;
            
            StartCoroutine(ShieldRoutine());
            
            G.SkillResources.ConsumeResources(AbilityDataCms.Instance.GetSpellConfig(SkillType.Shield).cost);
        }

        public void SetHealAfterHealthDrop(int amount)
        {
            _healAfterHealthDrop = amount;
        }

        public void SetRegenerate(bool regenerate)
        {
            _isRegenerating = regenerate;
        }

        public void SetRegenerateAmount(int amount)
        {
            _regenerationAmount = amount;
        }

        public void LoseHpAtStart(bool b)
        {
            _isLosingHealthAtStart = b;
        }
    }
}