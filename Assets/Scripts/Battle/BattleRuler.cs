using System;
using System.Collections.Generic;
using EntityResources;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle
{
    public class BattleRuler : MonoBehaviour
    {
        [SerializeField] private bool _isReturnToCity;
        [SerializeField] private GameObject _victoryPanel;

        private readonly List<Enemy> _enemiesOnScene = new List<Enemy>();
        
        private bool _isFighting = false;
        private int _defietedEnemies = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            _enemiesOnScene.AddRange(FindObjectsOfType<Enemy>());
        }

        private void Start()
        {
            G.SmoothSlideY.Show();
        }

        private void OnEnable()
        {
            foreach (var enemy in _enemiesOnScene)
            {
                enemy.GetComponent<Hp>().OnDeath += CheckVictory;
            }
        }

        private void OnDisable()
        {
            /*foreach (var enemy in _enemiesOnScene)
            {
                enemy.GetComponent<Hp>().OnDeath -= CheckVictory;
            }*/
        }
        
        private void Victory()
        {
            _isFighting = false;
            //Анимация победы?
            _victoryPanel.SetActive(true);
        }

        private void CheckVictory()
        {
            _defietedEnemies++;

            if (_defietedEnemies >= _enemiesOnScene.Count)
            {
                Victory();
            }
        }
        
        public static BattleRuler Instance;
        public bool IsFighting => _isFighting;
        public Action OnFighting;
        
        public void StartFight()
        {
            _isFighting = true;
            
            OnFighting?.Invoke();
        }
        
        public void ChangeScene()
        {
            if (_isReturnToCity)
            {
                // SceneManager.LoadScene(id scene of city);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}
