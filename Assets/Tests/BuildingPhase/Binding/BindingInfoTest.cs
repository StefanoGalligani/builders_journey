using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.BuildingPhase.Binding {
    public class BindingInfoTest
    {
        private GameObject obj;
        private BindingInfo bindingInfo;
        private int index;
        private void AssignIndex(int actionIndex) {
            index = actionIndex;
        }

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            bindingInfo = obj.AddComponent<BindingInfo>();
            bindingInfo.Init(null, null, 5);
            index = -2;
        }
        
        [Test]
        public void TestOnRebindBtnPressed()
        {
            bindingInfo.OnRebind += AssignIndex;
            bindingInfo.OnRebindBtnPressed();
            Assert.AreEqual(5, index);
        }

        [TearDown]
        public void TearDown() {
            bindingInfo = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
