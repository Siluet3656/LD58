using System;
using Battle;
using TMPro;
using UnityEngine;

namespace View
{
    public class FloatingTextClick : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Vector3 _offset = new Vector3(0, 2f, 0);

        private bool _clicked = false;

        private void Awake()
        {
            G.ClickFloatingTexts.Add(this);
        }

        void Start()
        {
            transform.position += _offset;
        }

        void Update()
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

        public void SetText(string message)
        {
            _textMesh.text = message;
        }

        public void OnLBM()
        {
            _clicked = true;
        }
        
        
    }
}