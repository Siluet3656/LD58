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
            foreach (char letter in sentence)
            {
                _textMesh.text += letter;
                yield return null;
            }
        }

        public void SetText(string message)
        {
            StartCoroutine(TypeSentence(message));
        }

        public void SetTitle(string title, VoiceType voice)
        {
            _titleMesh.text = title;

            switch (voice)
            {
                case VoiceType.Man:
                    _manSoundPlayer.PlayRandomSound();
                    break;
                case VoiceType.Woman:
                    _womanSoundPlayer.PlayRandomSound();
                    break;
                case VoiceType.Alien:
                    _alienSoundPlayer.PlayRandomSound();
                    break;
            }
        }

        public void OnLBM()
        {
            _clicked = true;
        }
    }
}