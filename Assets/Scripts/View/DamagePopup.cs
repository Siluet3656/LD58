using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class DamagePopup : MonoBehaviour
    {
        #region Singleton

        public static DamagePopup Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        [SerializeField] private int _popupPoolSize;
        [SerializeField] private Text _textPrefab;
        
        private Camera _mainCamera;
        private Transform _myTransform;

        private Queue<Text> _popupPool;
        private List<ActiveText> _activeTexts;

        private void Start()
        {
            _mainCamera = Camera.main;
            _myTransform = transform;
            _popupPool = new Queue<Text>();
            _activeTexts = new List<ActiveText>();

            for (int i = 0; i < _popupPoolSize; i++)
            {
                Text temp = Instantiate(_textPrefab, _myTransform);
                temp.gameObject.SetActive(false);
                _popupPool.Enqueue(temp);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _activeTexts.Count; i++)
            {
                ActiveText activeText = _activeTexts[i];
                
                if (activeText.DecreaseTimerAndCheckIt(Time.deltaTime))
                {
                    activeText.UiText.gameObject.SetActive(false);
                    _popupPool.Enqueue(activeText.UiText);
                    _activeTexts.Remove(activeText);
                    --i;
                }
                else
                {
                    activeText.FadeOut();
                    activeText.MoveText(_mainCamera);
                }
            }
        }

        public void AddText(int damageAmount, Vector3 unitPosition)
        {
            Text text = _popupPool.Dequeue();
            text.text = damageAmount.ToString(CultureInfo.InvariantCulture);
            text.gameObject.SetActive(true);

            ActiveText activeText = new ActiveText(1f, text, unitPosition);
            activeText.MoveText(_mainCamera);
            _activeTexts.Add(activeText);
        }
        
        public void AddText(string message, Vector3 unitPosition)
        {
            Text text = _popupPool.Dequeue();
            text.text = message;
            text.gameObject.SetActive(true);

            ActiveText activeText = new ActiveText(1f, text, unitPosition);
            activeText.MoveText(_mainCamera);
            _activeTexts.Add(activeText);
        }
    }
}
