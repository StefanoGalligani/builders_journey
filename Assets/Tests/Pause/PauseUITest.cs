using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.Pause
{
    public class PauseUITest
    {
        private GameObject obj;
        private GameObject content;
        private GameObject settings;
        private PauseUI pauseObj;

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            content = new GameObject();
            settings = new GameObject();
            obj.AddComponent<PauseUI>();
            pauseObj = obj.GetComponent<PauseUI>();
            pauseObj._content = content;
            pauseObj._settings = settings;
            
            pauseObj.Start();
        }

        [Test]
        public void TestInitialState() {
            Assert.False(content.activeSelf);
            Assert.AreEqual(1, Time.timeScale);
        }

        [Test]
        public void TestPauseToggle() {
            pauseObj.OnPause();
            Assert.True(Time.timeScale == 0);
            Assert.True(content.activeSelf);
            Assert.False(settings.activeSelf);

            pauseObj.OnPause();
            Assert.True(Time.timeScale == 1);
            Assert.False(content.activeSelf);
            Assert.False(settings.activeSelf);
        }

        [Test]
        public void TestSettingsToggle() {
            pauseObj.OnToggleSettings(true);
            Assert.False(settings.activeSelf);

            pauseObj.OnPause();

            pauseObj.OnToggleSettings(true);
            Assert.True(settings.activeSelf);
            pauseObj.OnToggleSettings(false);
            Assert.False(settings.activeSelf);
        }

        [TearDown]
        public void TearDown() {
            GameObject.DestroyImmediate(pauseObj);
            GameObject.DestroyImmediate(obj);
            GameObject.DestroyImmediate(content);
            GameObject.DestroyImmediate(settings);
        }
    }
}
