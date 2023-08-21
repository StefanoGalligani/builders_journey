using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.EndingPhase {
    public class EndUITest
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
        public void Test()
        {
            Assert.Fail();
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
