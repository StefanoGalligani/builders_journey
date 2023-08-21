using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.EndingPhase {
    public class EndNotifierTest
    {
        private GameObject endObj;
        private EndNotifier endNotifier;
        private GameObject other;
        private Collider2D otherCollider;
        private int n;
        private void IncreaseN() {
            n++;
        }

        [SetUp]
        public void SetUp() {
            endObj = new GameObject();
            endNotifier = endObj.AddComponent<EndNotifier>();
            other = new GameObject();
            otherCollider = other.AddComponent<BoxCollider2D>();
            other.layer = LayerMask.NameToLayer("Vehicle");
            n=0;
        }
        
        [Test]
        public void TestEventCalled()
        {
            endNotifier.GameEnd += IncreaseN;
            endNotifier.OnTriggerEnter2D(otherCollider);
            Assert.AreEqual(1,n);
        }
        
        [Test]
        public void TestEventOnlyCalledOnce()
        {
            endNotifier.GameEnd += IncreaseN;
            endNotifier.OnTriggerEnter2D(otherCollider);
            endNotifier.OnTriggerEnter2D(otherCollider);
            Assert.AreEqual(1,n);
        }
        
        [Test]
        public void TestEventNotCalledWhenWrongLayer()
        {
            other.layer = LayerMask.NameToLayer("Default");
            endNotifier.GameEnd += IncreaseN;
            endNotifier.OnTriggerEnter2D(otherCollider);
            Assert.AreEqual(0,n);
        }

        [TearDown]
        public void TearDown() {
            endNotifier = null;
            otherCollider = null;
            GameObject.DestroyImmediate(endObj);
            GameObject.DestroyImmediate(other);
            n=0;
        }
    }
}
