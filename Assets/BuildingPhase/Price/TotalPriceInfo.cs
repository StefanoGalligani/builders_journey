using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Price
{
    public class TotalPriceInfo : MonoBehaviour
    {
        [SerializeField] PriceUI _priceUi;
        private int _totalPrice = 0;

        public void SumPrice(int price) {
            if (price > 0) {
                _totalPrice += price;
                _priceUi.UpdatePrice(_totalPrice);
            }
        }
        
        public void SubtractPrice(int price) {
            if (price > 0) {
                _totalPrice -= price;
                _priceUi.UpdatePrice(_totalPrice);
            }
        }

        public int GetTotalPrice() {
            return _totalPrice;
        }
    }
}
