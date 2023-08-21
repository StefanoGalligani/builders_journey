using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.BuildingPhase.Price {
    public class TotalPriceInfoTest
    {
        private GameObject obj;
        private TotalPriceInfo priceInfo;

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            priceInfo = obj.AddComponent<TotalPriceInfo>();
        }
        
        [Test]
        public void TestSum()
        {
            priceInfo.SumPrice(3);
            priceInfo.SumPrice(7);
            priceInfo.SumPrice(-5);
            Assert.AreEqual(10, priceInfo.GetTotalPrice());
        }
        
        [Test]
        public void TestSubtract()
        {
            priceInfo.SubtractPrice(3);
            priceInfo.SubtractPrice(7);
            priceInfo.SubtractPrice(-5);
            Assert.AreEqual(-10, priceInfo.GetTotalPrice());
        }

        [TearDown]
        public void TearDown() {
            priceInfo = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
