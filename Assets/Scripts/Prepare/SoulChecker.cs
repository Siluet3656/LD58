using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prepare
{
    public class SoulChecker : MonoBehaviour
    {
        [SerializeField] private Text _costText;
        [SerializeField] private int _maxCost;

        private bool _isSoulPlacingBlocked = false;
        
        private void Awake()
        {
            G.SoulChecker = this;
        }

        private void Update()
        {
            int sum = 0;
            foreach (var soulPlace in G.SoulPlaces)
            {
                if (soulPlace.GetSoulCost() > 0)
                    sum += soulPlace.GetSoulCost();
            }

            if (sum >= _maxCost)
            {
                _isSoulPlacingBlocked = true;
            }
            else
            {
                _isSoulPlacingBlocked = false;
            }
            
            _costText.text = $"{sum} / {_maxCost}";
        }
        
        public bool IsSoulPlacingBlocked =>  _isSoulPlacingBlocked;
    }
}
