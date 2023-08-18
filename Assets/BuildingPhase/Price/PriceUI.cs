using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.Start;
using BuilderGame.Levels;
using TMPro;

namespace BuilderGame.BuildingPhase.Price
{
    public class PriceUI : MonoBehaviour
    {
        [SerializeField] GameObject _content;
        [SerializeField] TextMeshProUGUI _threeStarsPrice;
        [SerializeField] TextMeshProUGUI _twoStarsPrice;
        [SerializeField] TextMeshProUGUI _totalPrice;
        [SerializeField] Color[] _textColorByStars;
        private int[] _starPricesLimits;
        
        private void Start() {
            _starPricesLimits = LevelReferenceSingleton.Instance.GetCurrentScenePriceLimits();
            _threeStarsPrice.text = _starPricesLimits[0] + " $";
            _twoStarsPrice.text = _starPricesLimits[1] + " $";
            _totalPrice.text = "0 $";
            _threeStarsPrice.color = _textColorByStars[2];
            _twoStarsPrice.color = _textColorByStars[1];
            _totalPrice.color = _textColorByStars[2];
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        internal void UpdatePrice(int newPrice) {
            _totalPrice.text = newPrice + " $";
            int stars = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(newPrice);
            _totalPrice.color = _textColorByStars[stars-1];
        }

        private void OnGameStart() {
            _content.SetActive(false);
        }
    }
}
