using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialToggleTest
    {
        private GameObject obj;
        private TutorialToggle tutorialToggle;
        private Toggle toggle;
        
        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            tutorialToggle = obj.AddComponent<TutorialToggle>();
            toggle = obj.AddComponent<Toggle>();
            tutorialToggle.toggle = toggle;
        }

        [Test]
        public void TestToggleOnAtStart()
        {
            PlayerPrefs.SetInt("TutorialEnabled", 1);
            tutorialToggle.Awake();
            Assert.True(toggle.isOn);
        }

        [Test]
        public void TestToggleOffAtStart()
        {
            PlayerPrefs.SetInt("TutorialEnabled", 0);
            tutorialToggle.Awake();
            Assert.False(toggle.isOn);
        }

        [Test]
        public void TestEnableOnToggle()
        {
            PlayerPrefs.SetInt("TutorialEnabled", 0);
            tutorialToggle.Awake();
            toggle.isOn = true;
            Assert.AreEqual(1, PlayerPrefs.GetInt("TutorialEnabled", 0));
        }

        [Test]
        public void TestDisableOnToggle()
        {
            PlayerPrefs.SetInt("TutorialEnabled", 1);
            tutorialToggle.Awake();
            toggle.isOn = false;
            Assert.AreEqual(0, PlayerPrefs.GetInt("TutorialEnabled", 1));
        }
        
        [TearDown]
        public void TearDown() {
            toggle = null;
            tutorialToggle = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
