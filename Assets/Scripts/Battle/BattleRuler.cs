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
        //[SerializeField] private bool _isReturnToCity;
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private GameObject _defeatPanel;
        [SerializeField] private GameObject _floatingTextPrefab;
        [SerializeField] private int _sceneID;
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private Text _tutorialText;

        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _fightButton;
        [SerializeField] private GameObject _putSoul;
        [SerializeField] private GameObject _targetSwitch;
        [SerializeField] private GameObject _handAnim;
        [SerializeField] private GameObject _anotherTutorialText;

        private Animator _tutorialPanelAnimator;

        private bool _isFighting = false;
        private int _defietedEnemies = 0;
        private bool _dialogueSkiped = false;
        private bool _tutorEnd = false;
        private bool _afterbattleend = false;

        private const string TutorialText1 = "Your health. If it reaches 0, your journey ends.";

        private const string TutorialText2 =
            "Your auto-attack power. \nAuto-attacks are performed automatically every 1.5 seconds.";

        private const string TutorialText3 = "The white bar below your health bar indicates auto-attack progress.";

        private const string TutorialText4 = "All souls you've collected will appear here.";
        private const string TutorialText5 = "You can drag and drop souls into upgrade slots.";
        private const string TutorialText6 = "Upgrade has limits on how many souls can be used on total capacity.";

        private const string TutorialText7 = "Here’s your abilities panel.";

        private const string TutorialText8 =
            "Energy is consumed when using abilities. It recovers at 20 Energy per second.";

        private const string TutorialText9 = "Click the ability icon or press (Q) to use it.";

        private static readonly int StartTutorial1 = Animator.StringToHash("StartTutorial1");
        private static readonly int StartTutorial2 = Animator.StringToHash("StartTutorial2");
        private static readonly int StartTutorial3 = Animator.StringToHash("StartTutorial3");
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

            EnemiesOnScene.AddRange(_enemies);

            if (_tutorialPanel != null)
                _tutorialPanelAnimator = _tutorialPanel.GetComponent<Animator>();
        }

        private void ShowDialog(string message, string charecterName, VoiceType voiceType)
        {
            GameObject floatingText = Instantiate(_floatingTextPrefab, transform.position, Quaternion.identity);
            floatingText.GetComponent<FloatingTextClick>().SetTitle(charecterName, voiceType);
            floatingText.GetComponent<FloatingTextClick>().SetText(message);
        }

        private IEnumerator StartGameTutorial1()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("Is this a city?\n I heard they have these kinds of things on Earth.", "Guest from afar",
                VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("I hope it's worth it.", "Guest from afar", VoiceType.Alien);

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

            ShowDialog("Who are you?\n What’s a thing like you doing around here?", "Victor", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Are you familiar with the concept of a soul?", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("What? What do you think you are?\n You’re gonna see souls with your own eyes.", "Victor",
                VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _enemies[0].IsNeedToGo = false;

            G.SmoothSlideY.Show();
        }

        private IEnumerator StartGameTutorial2()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("Hey! What have you done to him?!", "Alice", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("His soul is now part of my collection.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("If you don't want to end up where he did,\nyou'd better not get in my way.", "Guest from afar",
                VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Uh-huh, like we’d believe you.", "Alice", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("You're exactly the kind who waits to stab someone in the back.", "Alex", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            G.SmoothSlideY.Show();
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial2);
            _tutorialText.text = TutorialText4;

            yield return new WaitForSeconds(1.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = TutorialText5;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;


            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = TutorialText6;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            _tutorEnd = true;
            _targetSwitch.SetActive(true);
            _putSoul.SetActive(true);
        }

        private IEnumerator StartGameTutorial3()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("Hey, we’re not your enemies, dude. Got a little mixed up—everyone makes mistakes.", "Anton",
                VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("These souls of yours are very interesting. They’ll surely help me achieve the Pure Soul.",
                "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Oh man, bro, looks like we’ve really messed up.", "Nikita", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            G.SmoothSlideY.Show();
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial3);
            _tutorialText.text = TutorialText7;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = TutorialText8;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = TutorialText9;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            _tutorEnd = true;
        }

        private IEnumerator StartDialogueAct0Fight4()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("We’re just simple hermits. There’s nothing to take from us.", "Igor", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("I could always take your soul — and right now it's the oddest one I've come across.",
                "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Alright, all of us, together — now we must finally stop him!", "Igor", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct1Fight5()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("It seems I've reached the city's entrance.", "Guest from Afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Countless souls, yet none shine with truth.", "Guest from Afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("I must continue. Somewhere among them lies the Pure Soul.", "Guest from Afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _enemies[0].IsNeedToGo = true;

            ShowDialog("Halt, intruder!! Don't think your crimes went unnoticed!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Her soul... it resonates differently from the rest.", "Guest from Afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Stop mumbling like I can't hear you!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Guards! We have a situation here!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _enemies[1].IsNeedToGo = true;
            _enemies[2].IsNeedToGo = true;

            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator Act1Fight5AfterBattle()
        {
            ShowDialog("Hostiles neutralized. Her signal is fading...", "Guest from afar", VoiceType.Alien);
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("She's retreating... I must find her again.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _afterbattleend = true;
        }

        private IEnumerator StartDialogueAct1Fight6()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("Welcome, stranger! Looking to buy? We trade in everything — even souls.", "Richard",
                VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Sell, buy, bargain — same thing. I have anything you need if you have enough money of course.",
                "Richard", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("I take what I need. I do not pay.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct1Fight7()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog("I can sense the Pure Soul near you. You're hiding it.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("You defile the sacred with your greed! The Pure Soul will never be yours!", "Ashley",
                VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("You're cursed! Show no mercy!", "Anna", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("It blasphemes! Offer your souls for purification!", "Andrey", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct1Fight8()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog("She... doesn’t... understand...", "Konstantin", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("I’m in pursuit of the Pure Soul. Step aside if you wish to live.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("I no longer seek any soul... except the Pure one.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Soul... pure... not... pure...", "Konstantin", VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("It’s no use. He can’t hear me.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct1Fight9()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog("Halt, intruder! Your greed consumes you!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("It’s not greed. It’s the experiment… the data must be complete.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Enough of your lies! Your corruption ends here!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Heh... finally found you. Nowhere to run. Nowhere to hide.", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct2Fight10()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog("She's escaping... again...", "Guest from afar", VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Help me! Someone—help me, I command you!!", "Victoria", VoiceType.Woman);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Yes, Your Holiness!", "Pakhoma", VoiceType.Woman);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct2Fight11()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog("Help me! Someone—help me, I command you!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Yes, Your Holiness!", "Henry", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }
        
        private IEnumerator StartDialogueAct2Fight12()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog("Help me! Someone—help me, I command you!!", "Victoria", VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Huh?! We’re in the middle of something here.", "Patrick", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Hey! Don’t run in here!", "Bob", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("...", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("...", "Patrick", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Wait... you’re not one of us, are you?", "Patrick", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Not your concern. Get out of my way.", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("You’re talking to the heads of the Soul Project, pal!", "Bob", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Exactly. And since you’ve seen our work... we can’t let you leave alive.", "Patrick", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Heh, yeah. No witnesses — no problem.", "Bob", VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct2Fight13()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog("Those two were leaders of some kind of project...", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("What kind of experiments were they conducting here?", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Their souls... they look unnatural.", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Even I couldn’t achieve results like this — not after years of experimentation.", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("Definitely... this expedition was worth it.", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("Footsteps echo nearby", "", VoiceType.None);

            foreach (Enemy enemy in G.Enemies)
            {
                enemy.IsNeedToGo = true;
            }
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog("...", "Scientist", VoiceType.None);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog("I'm not alone.", "Guest from afar", VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            foreach (Enemy enemy in G.Enemies)
            {
                enemy.IsNeedToGo = false;
            }
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
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
                    StartCoroutine(StartDialogueAct0Fight4());
                    break;
                case 5:
                    StartCoroutine(StartDialogueAct1Fight5());
                    break;
                case 6:
                    StartCoroutine(StartDialogueAct1Fight6());
                    break;
                case 7:
                    StartCoroutine(StartDialogueAct1Fight7());
                    break;
                case 8:
                    StartCoroutine(StartDialogueAct1Fight8());
                    break;
                case 9:
                    StartCoroutine(StartDialogueAct1Fight9());
                    break;
                case 10:
                    StartCoroutine(StartDialogueAct2Fight10());
                    break;
                case 11:
                    StartCoroutine(StartDialogueAct2Fight11());
                    break;
                case 12:
                    StartCoroutine(StartDialogueAct2Fight12());
                    break;
                case 13:
                    StartCoroutine(StartDialogueAct2Fight13());
                    break;
                default:
                    G.SmoothSlideY.Show();
                    break;
            }
            
        }

        private void Update()
        {
            if (_sceneID == 2)
            {
                if (G.SoulChecker.IsSoulPlacingBlocked)
                {
                    _putSoul.SetActive(false);
                    _fightButton.SetActive(true);
                    _handAnim.SetActive(false);
                }
                else
                {
                    _putSoul.SetActive(true);
                    _fightButton.SetActive(false);
                    if (_tutorEnd)
                        _handAnim.SetActive(true);
                }

                if (_tutorEnd)
                {
                    _targetSwitch.SetActive(true);
                }
            }

            if (_afterbattleend)
            {
                Victory();
            }
        }

        private void OnEnable()
        {
            foreach (var enemy in EnemiesOnScene)
            {
                enemy.GetComponent<Hp>().OnDeath += CheckVictory;
                enemy.GetComponent<Enemy>().OnRetreat += CheckVictory;
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
        
        private IEnumerator LoadSceneWithFade()
        {
            yield return StartCoroutine(G.ScreenFader.FadeOut());
            LoadScene();
        }

        private void LoadScene()
        {
            GameState.State++;
            SceneManager.LoadScene(1);
        }

        private void Defeat()
        {
            _isFighting = false;
            _defeatPanel.SetActive(true);
            _player.GetComponent<PlayerTargeting>().ClearArrow();
        }
        
        private void Victory()
        {
            _isFighting = false;
            //Анимация победы?
            _victoryPanel.SetActive(true);
            
            if (_sceneID == 3)
            {
                _handAnim.SetActive(false);
            }
        }

        private void CheckVictory()
        {
            _defietedEnemies++;

            if (_defietedEnemies >= EnemiesOnScene.Count)
            {
                if (_sceneID == 5)
                {
                    _isFighting = false;
                    G.Player.GetComponent<PlayerTargeting>().ClearArrow();
                    StartCoroutine(Act1Fight5AfterBattle());
                    _dialogueSkiped = false;
                }
                else
                {
                    Victory();
                }
            }
        }
        
        public static BattleRuler Instance;
        public bool IsFighting => _isFighting;
        public Action OnFighting;
        public bool IsLBM = false;
        public bool DialogueSkipped => _dialogueSkiped;
        public readonly List<Enemy> EnemiesOnScene = new List<Enemy>();
        public bool IsCastStarted;
        
        public void StartFight()
        {
            _isFighting = true;
            
            OnFighting?.Invoke();

            if (_sceneID == 3)
            {
                _handAnim.SetActive(true);
                _anotherTutorialText.SetActive(true);
            }
        }
        
        public void ChangeScene()
        {
            StartCoroutine(LoadSceneWithFade());
        }

        public void DialogueSkip()
        {
            StopAllCoroutines();

            foreach (Enemy enemy in _enemies)
            {
                enemy.IsNeedToGo = true;
            }
            
            G.SmoothSlideY.Show();

            _dialogueSkiped = true;
            _tutorEnd = true;
        }
    }
}
