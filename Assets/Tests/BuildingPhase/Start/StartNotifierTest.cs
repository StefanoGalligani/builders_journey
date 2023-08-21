using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.BuildingPhase.Start {
    public class StartNotifierTest
    {
        private GameObject obj;
        private StartNotifier startNotifier;
        private bool notified;
        private void ReceiveNotification() {
            notified = true;
        }

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            startNotifier = obj.AddComponent<StartNotifier>();
            notified = false;
        }
        
        [Test]
        public void TestDontNotifyIfNotPermitted()
        {
            startNotifier.GameStart += ReceiveNotification;
            startNotifier.StartGame();
            Assert.False(notified);
        }
        
        [Test]
        public void TestNotifyWhenPermitted()
        {
            startNotifier.GameStart += ReceiveNotification;
            startNotifier.CanStart = true;
            startNotifier.StartGame();
            Assert.True(notified);
        }

        [TearDown]
        public void TearDown() {
            startNotifier = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
