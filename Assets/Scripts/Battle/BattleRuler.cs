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
        [SerializeField] private GameObject _victoriaPureSoul;
        
        //private ExistAndDestroy _pop;

        private Animator _tutorialPanelAnimator;

        private bool _isFighting = false;
        private int _defietedEnemies = 0;
        private bool _dialogueSkiped = false;
        private bool _tutorEnd = false;
        private bool _afterbattleend = false;

        private string _tutorialText1;
        private string _tutorialText2;
        private string _tutorialText3;
        private string _tutorialText4;
        private string _tutorialText5;
        private string _tutorialText6;
        private string _tutorialText7;
        private string _tutorialText8;
        private string _tutorialText9;
        private string _tutorialText10;
        private string _tutorialText11;

        private static readonly int StartTutorial1 = Animator.StringToHash("StartTutorial1");
        private static readonly int StartTutorial2 = Animator.StringToHash("StartTutorial2");
        private static readonly int StartTutorial3 = Animator.StringToHash("StartTutorial3");
        private static readonly int StartTutorial4 = Animator.StringToHash("StartTutorial4");
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

            IsVictory = false;

            EnemiesOnScene.AddRange(_enemies);

            if (_tutorialPanel != null)
                _tutorialPanelAnimator = _tutorialPanel.GetComponent<Animator>();
            
            _tutorialText1 = LocalizationManager.Instance.Get("TutorialText1");
            _tutorialText2 = LocalizationManager.Instance.Get("TutorialText2");
            _tutorialText3 = LocalizationManager.Instance.Get("TutorialText3");
            _tutorialText4 = LocalizationManager.Instance.Get("TutorialText4");
            _tutorialText5 = LocalizationManager.Instance.Get("TutorialText5");
            _tutorialText6 = LocalizationManager.Instance.Get("TutorialText6");
            _tutorialText7 = LocalizationManager.Instance.Get("TutorialText7");
            _tutorialText8 = LocalizationManager.Instance.Get("TutorialText8");
            _tutorialText9 = LocalizationManager.Instance.Get("TutorialText9");
            _tutorialText10 = LocalizationManager.Instance.Get("TutorialText10");
            _tutorialText11 = LocalizationManager.Instance.Get("TutorialText11");

            //_pop = Resources.Load<ExistAndDestroy>("Prefabs/FloatingSoulPOP");
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

            ShowDialog(LocalizationManager.Instance.Get("DialogueText1"),
                LocalizationManager.Instance.Get("nameGuest"),
                VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText2"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial1);
            _tutorialText.text = _tutorialText1;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText2;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText3;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanel.SetActive(false);

            _enemies[0].IsNeedToGo = true;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText3"),
                LocalizationManager.Instance.Get("nameVictor"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText4"), 
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText5"),
                LocalizationManager.Instance.Get("nameVictor"),
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

            ShowDialog(LocalizationManager.Instance.Get("DialogueText6"),
                LocalizationManager.Instance.Get("nameAlice"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText7"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText8"), 
                LocalizationManager.Instance.Get("nameGuest"),
                VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText9"),
                LocalizationManager.Instance.Get("nameAlice"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText10"),
                LocalizationManager.Instance.Get("nameAlex"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            G.SmoothSlideY.Show();
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial2);
            _tutorialText.text = _tutorialText4;

            yield return new WaitForSeconds(1.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText5;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;


            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText6;

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

            ShowDialog(LocalizationManager.Instance.Get("DialogueText11"),
                LocalizationManager.Instance.Get("nameAnton"),
                VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText12"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText13"),
                LocalizationManager.Instance.Get("nameNikita"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            G.SmoothSlideY.Show();
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial3);
            _tutorialText.text = _tutorialText7;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText8;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText9;

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

            ShowDialog(LocalizationManager.Instance.Get("DialogueText14"), 
                LocalizationManager.Instance.Get("nameIgor"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText15"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText16"),
                LocalizationManager.Instance.Get("nameIgor"), VoiceType.Man);

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

            ShowDialog(LocalizationManager.Instance.Get("DialogueText17"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText18"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText19"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _enemies[0].IsNeedToGo = true;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText20"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText21"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText22"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText23"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _enemies[1].IsNeedToGo = true;
            _enemies[2].IsNeedToGo = true;
            
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(StartTutorial4);
            _tutorialText.text = _tutorialText10;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            _tutorialPanel.SetActive(true);
            _tutorialPanelAnimator.SetTrigger(TutorialStep);
            _tutorialText.text = _tutorialText11;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _tutorialPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator Act1Fight5AfterBattle()
        {
            ShowDialog(LocalizationManager.Instance.Get("DialogueText24"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText25"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            _afterbattleend = true;
        }

        private IEnumerator StartDialogueAct1Fight6()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText26"),
                LocalizationManager.Instance.Get("nameRichard"),
                VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText27"),
                LocalizationManager.Instance.Get("nameRichard"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText28"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

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

            ShowDialog(LocalizationManager.Instance.Get("DialogueText29"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText30"),
                LocalizationManager.Instance.Get("nameAshley"),
                VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText31"),
                LocalizationManager.Instance.Get("nameAnna"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText32"),
                LocalizationManager.Instance.Get("nameAndrey"), VoiceType.Man);

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
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText33"), 
                LocalizationManager.Instance.Get("nameKonstantin"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText34"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText35"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText36"),
                LocalizationManager.Instance.Get("nameKonstantin"), VoiceType.Man);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText37"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

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
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText38"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText39"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText40"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText41"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

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
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText42"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText43"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);
            
            G.VictoriaEscape.Flip();
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText44"),
                LocalizationManager.Instance.Get("namePakhoma"), VoiceType.Woman);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.VictoriaEscape.Escape();
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct2Fight11()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText45"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText46"),
                LocalizationManager.Instance.Get("nameHenry"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.VictoriaEscape.Flip();
            G.VictoriaEscape.Escape();
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }
        
        private IEnumerator StartDialogueAct2Fight12()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText47"), 
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.VictoriaEscape.Flip();
            G.VictoriaEscape.Escape();
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText48"),
                LocalizationManager.Instance.Get("namePatrick"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText49"),
                LocalizationManager.Instance.Get("nameBob"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText50"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText51"), 
                LocalizationManager.Instance.Get("namePatrick"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText52"), 
                LocalizationManager.Instance.Get("namePatrick"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText53"), 
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText54"), 
                LocalizationManager.Instance.Get("nameBob"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText55"), 
                LocalizationManager.Instance.Get("namePatrick"), VoiceType.Man);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText56"), 
                LocalizationManager.Instance.Get("nameBob"), VoiceType.Man);
            
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
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText57"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText58"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText59"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText60"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText61"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText62"),
                "", VoiceType.None);

            foreach (Enemy enemy in G.Enemies)
            {
                enemy.IsNeedToGo = true;
            }
            
            G.Steps.GoSteps();
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText63"), 
                LocalizationManager.Instance.Get("nameScientist"), VoiceType.None);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText64"), 
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
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

        private IEnumerator StartDialogueAct2Fight14()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText65"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText66"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText67"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText68"),
                LocalizationManager.Instance.Get("nameVictoria"), VoiceType.Woman);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.VictoriaEscape.Flip();
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText69"),
                "", VoiceType.None);
            
            yield return new WaitForSeconds(1.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            G.VictoriaEscape.Escape();
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText70"),
                LocalizationManager.Instance.Get("nameVictoria2"), VoiceType.Woman);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct2Fight15()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText71"), 
                "", VoiceType.None);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText72"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText73"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText74"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText75"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText76"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText77"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText78"),
                "", VoiceType.None);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;

            ShowDialog(LocalizationManager.Instance.Get("DialogueText79"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText80"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText81"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText82"),
                LocalizationManager.Instance.Get("nameArchivist"), VoiceType.None); //VoiceType.AI
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText83"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText84"),
                LocalizationManager.Instance.Get("nameArchivist"), VoiceType.None); //VoiceType.AI
            
            G.SpawnCrystals.SpawnCrystal();
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct3Fight16()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText85"),
                "", VoiceType.None);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText86"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText87"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText88"),
                "", VoiceType.None);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct3Fight17()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText89"),
                "", VoiceType.None);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText90"),
                LocalizationManager.Instance.Get("nameGuest"), VoiceType.Alien);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText91"),
                "", VoiceType.None);

            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
            GameObject.FindGameObjectWithTag("SKIP").SetActive(false);
            G.SmoothSlideY.Show();
        }

        private IEnumerator StartDialogueAct3Fight18()
        {
            yield return new WaitForSeconds(2f);
            IsLBM = false;
            
            ShowDialog(LocalizationManager.Instance.Get("DialogueText92"),
                "", VoiceType.None);
            
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => IsLBM);
            IsLBM = false;
            
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
                case 14:
                    StartCoroutine(StartDialogueAct2Fight14());
                    break;
                case 15:
                    StartCoroutine(StartDialogueAct2Fight15());
                    break;
                case 16:
                    StartCoroutine(StartDialogueAct3Fight16());
                    break;
                case 17:
                    StartCoroutine(StartDialogueAct3Fight17());
                    break;
                case 18:
                    StartCoroutine(StartDialogueAct3Fight18());
                    break;
                default:
                    if (G.SmoothSlideY)
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
            if (EnemiesOnScene.Count > 0)
            {
                foreach (var enemy in EnemiesOnScene)
                {
                    enemy.GetComponent<Hp>().OnDeath += CheckVictory;
                    enemy.GetComponent<Enemy>().OnRetreat += CheckVictory;
                }
            }
            
            if (_player)
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
            /*foreach (var floatingSoul in G.SoulsManager.FloatingSouls)
            {
                if (floatingSoul.isActiveAndEnabled)
                    Instantiate(_pop, floatingSoul.transform.position, Quaternion.identity, null);
            }*/
            
            _isFighting = false;
            _defeatPanel.SetActive(true);
            _player.GetComponent<PlayerTargeting>().ClearArrow();
        }
        
        private void Victory()
        {
            _isFighting = false;
            //Анимация победы?
            _victoryPanel.SetActive(true);

            IsVictory = true;
            
            if (_sceneID == 3)
            {
                _handAnim.SetActive(false);
            }
            
            ArrowToTarget arrowToTarget = FindObjectOfType<ArrowToTarget>();
            if (arrowToTarget)
                arrowToTarget.gameObject.SetActive(false);
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
        public bool IsVictory;
        
        public void StartFight()
        {
            _isFighting = true;
            
            OnFighting?.Invoke();
            
            G.SoulsManager.StartFloat();

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
            if (G.VictoriaEscape)
                G.VictoriaEscape.Skip();
            
            if (_victoriaPureSoul)
                _victoriaPureSoul.SetActive(true);

            if (_sceneID == 15)
            {
                G.SpawnCrystals.SpawnCrystal();
            }
            
            G.SmoothSlideY.Show();

            _dialogueSkiped = true;
            _tutorEnd = true;
        }
    }
}
