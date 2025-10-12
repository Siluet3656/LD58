using System;
using System.Collections;
using System.Collections.Generic;
using EntityResources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Battle
{
    public class BattleRuler : MonoBehaviour
    {
        [SerializeField] private bool _isReturnToCity;
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private GameObject _defeatPanel;
        [SerializeField] private GameObject _floatingTextPrefab;
        [SerializeField] private int _sceneID;
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private Text _tutorialText;

        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private Player _player;

        private readonly List<Enemy> _enemiesOnScene = new List<Enemy>();
        
        private Animator _tutorialPanelAnimator;
        
        private bool _isFighting = false;
        private int _defietedEnemies = 0;
        private bool _dialogueSkiped = false;
        
        private const string TutorialText1 = "Your health. If it reaches 0, your journey ends.";
        private const string TutorialText2 = "Your auto-attack power. \nAuto-attacks are performed automatically every 1.5 seconds.";
        private const string TutorialText3 = "The white bar below your health bar indicates auto-attack progress.";
        
        private static readonly int StartTutorial1 = Animator.StringToHash("StartTutorial1");
        private static readonly int TutorialStep = Animator.StringToHash("TutorialStep");

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
            
            _enemiesOnScene.AddRange(_enemies);
            
            _tutorialPanelAnimator =  _tutorialPanel.GetComponent<Animator>();
        }
        
        private void ShowDialog(string message, bool isMe)
        {
            GameObject ft;
            if (isMe)
            {
                ft = Instantiate(_floatingTextPrefab, transform.position - new Vector3(transform.position.x + 8f, transform.position.y,transform.position.z), Quaternion.identity);
            }
            else
            {
                ft = Instantiate(_floatingTextPrefab, transform.position - new Vector3(transform.position.x - 8f, transform.position.y,transform.position.z), Quaternion.identity);
            }
            
            ft.GetComponent<FloatingTextClick>().SetText(message);
        }

        private IEnumerator StartGameTutorial1()
        {
            yield return new WaitForSeconds(0.5f);
            
            ShowDialog("Is this a city?\n I heard they have these kinds of things on Earth.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("I hope it's worth it.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial1);
            _tutorialText.text = TutorialText1;
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = TutorialText2;
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = TutorialText3;
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanel.SetActive(false);
            
            _enemies[0].IsNeedToGo = true;
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Who are you?\n What’s a thing like you doing around here?", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Are you familiar with the concept of a soul?", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("What? What do you think you are?\n You’re gonna see souls with your own eyes.", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _enemies[0].IsNeedToGo = false;
            
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartGameTutorial2()
        {
            yield return new WaitForSeconds(0.5f);
            
            ShowDialog("Hey! What have you done to him?!", false);
            
            yield return new WaitForSeconds(0.5f);
            IsLBM = false;
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            ShowDialog("His soul is now part of my collection.", true);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            ShowDialog("If you don't want to end up where he did,\nyou'd better not get in my way.", true);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            ShowDialog("Uh-huh, like we’d believe you.", false);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            ShowDialog("You're exactly the kind who waits to stab someone in the back.", false);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitUntil(() => IsLBM);
            
            G.SmoothSlideY.Show();
            
            //_tutorialPanel1.SetActive(true);
            
            yield return new WaitForSeconds(1.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            //_tutorialPanel1.SetActive(false);
            
            //_tutorialPanel2.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            //_tutorialPanel2.SetActive(false);
            
            //_tutorialPanel3.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            //_tutorialPanel3.SetActive(false);
        }
        
        private IEnumerator StartGameTutorial3()
        {
            yield return new WaitForSeconds(0.5f);
            
            ShowDialog("Hey, we’re not your enemies, dude. Got a little mixed up—everyone makes mistakes.", false);
            
            yield return new WaitForSeconds(0.5f);
            IsLBM = false;
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("These souls of yours are very interesting. They’ll surely help me achieve the Pure Soul.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Oh man, bro, looks like we’ve really messed up.", false);
            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            G.SmoothSlideY.Show();
            
            /*_tutorialPanel1.SetActive(true);
            
            yield return new WaitForSeconds(1.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanel1.SetActive(false);
            
            _tutorialPanel2.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanel2.SetActive(false);
            
            _tutorialPanel3.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanel3.SetActive(false);*/
        }

        private IEnumerator StartDialogue1()
        {
            ShowDialog("We’re just simple hermits. There’s nothing to take from us.", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("I could always take your soul — and right now it's the oddest one I've come across.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Alright, all of us, together — now we must finally stop him!", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.SmoothSlideY.Show();
        }
        
        private IEnumerator StartDialogue2()
        {
            ShowDialog("So… you’re the one who holds the Pure Soul.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Hold? No. I am the Pure Soul. There’s nothing left to hold onto.", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Then you’ve lost what it means to be human.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Human? Humanity is a cage of flesh and fear. I broke it long ago.", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("So, what is the Pure Soul?", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Emptiness. No pain. No doubt. No desire. Can you imagine how peaceful it feels?", false);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Worth it — just like I thought.", true);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.SmoothSlideY.Show();
        }
        
        private void Start()
        {
            switch (_sceneID)
            {
                case 1:
                    StartCoroutine(StartGameTutorial1());
                    break;
                case 2:
                    StartCoroutine(StartGameTutorial2());
                    break;
                case 3:
                    StartCoroutine(StartGameTutorial3());
                    break;
                case 4:
                    StartCoroutine(StartDialogue1());
                    break;
                case 5:
                    StartCoroutine(StartDialogue2());
                    break;
                default:
                    G.SmoothSlideY.Show();
                    break;
            }
            
        }

        private void OnEnable()
        {
            foreach (var enemy in _enemiesOnScene)
            {
                enemy.GetComponent<Hp>().OnDeath += CheckVictory;
            }
            
            _player.GetComponent<Hp>().OnDeath += Defeat;
        }

        private void OnDisable()
        {
            /*foreach (var enemy in _enemiesOnScene)
            {
                enemy.GetComponent<Hp>().OnDeath -= CheckVictory;
            }*/
        }

        private void Defeat()
        {
            _isFighting = false;
            _defeatPanel.SetActive(true);
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
        public bool IsLBM = false;
        
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

        public void DialogueSkip()
        {
            StopAllCoroutines();
            
            G.SmoothSlideY.Show();

            _dialogueSkiped = true;
        }

        public bool DialogueSkipped => _dialogueSkiped;
    }
}
