using System.Collections;
using Battle;
using TMPro;
using UnityEngine;

namespace View
{
    public class FloatingTextClick : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleMesh;
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Vector3 _offset = new Vector3(0, 2f, 0);
        
        [Header("Voices")]
        [SerializeField] private RandomSoundPlayer _manSoundPlayer;
        [SerializeField] private RandomSoundPlayer _womanSoundPlayer;
        [SerializeField] private RandomSoundPlayer _alienSoundPlayer;
        [SerializeField] private RandomSoundPlayer _aiSoundPlayer;

        private VoiceType _voiceType;
        private bool _clicked = false;

        private void Awake()
        {
            G.ClickFloatingTexts.Add(this);
        }

        private void Start()
        {
            transform.position += _offset;
        }

        private void Update()
        {
            if (BattleRuler.Instance.DialogueSkipped)
            {
                _clicked = true;
            }
            
            if (_clicked)
            {
                G.ClickFloatingTexts.Remove(this);
                Destroy(gameObject);
            }
        }

        private IEnumerator TypeSentence(string sentence)
        {
            _textMesh.text = "";
            int i = 0;
            foreach (char letter in sentence)
            {
                _textMesh.text += letter;
                
                if (i % 7 == 0)
                {
                    PlayVoiceSound(_voiceType);
                }
                i++;
                yield return null;
                yield return null;
            }
        }
        
        private void PlayVoiceSound(VoiceType voiceType)
        {
            switch (voiceType)
            {
                case VoiceType.Man:
                    _manSoundPlayer.StopPlay();
                    _manSoundPlayer.PlayRandomSound();
                    break;
                case VoiceType.Woman:
                    _womanSoundPlayer.StopPlay();
                    _womanSoundPlayer.PlayRandomSound();
                    break;
                case VoiceType.Alien:
                    _alienSoundPlayer.StopPlay();
                    _alienSoundPlayer.PlayRandomSound();
                    break;
                case VoiceType.AI:
                    _aiSoundPlayer.StopPlay();
                    _aiSoundPlayer.PlayRandomSound();
                    break;
            }
        }

        public void SetText(string message)
        {
            StartCoroutine(TypeSentence(message));
        }

        public void SetTitle(string title, VoiceType voice)
        {
            _voiceType = voice;
            _titleMesh.text = title;
        }

        public void OnLBM()
        {
            _clicked = true;
        }
    }
}