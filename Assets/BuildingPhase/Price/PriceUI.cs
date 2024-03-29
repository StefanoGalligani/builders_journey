using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase;
using BuilderGame.Levels;
using TMPro;

namespace BuilderGame.BuildingPhase.Price
{
    public class PriceUI : BuildingPhaseUI
    {
        [SerializeField] TextMeshProUGUI _threeStarsPrice;
        [SerializeField] TextMeshProUGUI _twoStarsPrice;
        [SerializeField] TextMeshProUGUI _totalPrice;
        [SerializeField] Color[] _textColorByStars;
        private LevelReference _levelReference;
        private int[] _starPricesLimits;
        private bool _isInitialized = false;
        private int _competitiveMode;
        
        protected override void Init() {
            if (_isInitialized) return;
            _isInitialized = true;
            
            _competitiveMode = PlayerPrefs.GetInt("CompetitiveMode", 0);
            if (_competitiveMode == 0) {
                ToggleContent(false);
            }

            _levelReference = FindObjectOfType<LevelReference>();
            _starPricesLimits = _levelReference.GetCurrentScenePriceLimits();
            if (_starPricesLimits == null) return;
            
            _threeStarsPrice.text = _starPricesLimits[0] + " $";
            _twoStarsPrice.text = _starPricesLimits[1] + " $";
            _totalPrice.text = "0 $";
            _threeStarsPrice.color = _textColorByStars[2];
            _twoStarsPrice.color = _textColorByStars[1];
            _totalPrice.color = _textColorByStars[2];
        }

        internal void UpdatePrice(int newPrice) {
            Init();
            _totalPrice.text = newPrice + " $";
            int stars = _levelReference.GetCurrentSceneLevelStars(newPrice);
            if (stars >= 1) _totalPrice.color = _textColorByStars[stars-1];
        }

        public override void EnableInTutorial() {
            ToggleContent(_competitiveMode == 1);
        }
    }
}
