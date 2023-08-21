using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;

namespace BuilderGame.Levels {
    public class LevelReferenceSingletonTest
    {
        private LevelInfoScriptableObject[] scriptableObjects;

        [SetUp]
        public void SetUp() {
            LevelInfoScriptableObject info1 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            LevelInfoScriptableObject info2 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            LevelInfoScriptableObject info3 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info1.LevelName = "Level1";
            info1.SceneName = "Scene1";
            info1.PriceLimitThreeStars = 1;
            info1.PriceLimitTwoStars = 10;
            info2.LevelName = "Level2";
            info2.SceneName = "Scene2";
            info2.PriceLimitThreeStars = 2;
            info2.PriceLimitTwoStars = 20;
            info3.LevelName = "Level3";
            info3.SceneName = "Scene3";
            info3.PriceLimitThreeStars = 3;
            info3.PriceLimitTwoStars = 30;
            scriptableObjects = new LevelInfoScriptableObject[] {info1, info2, info3};
            LevelReferenceSingleton.Instance.SetReferences(scriptableObjects);
            LevelReferenceSingleton.Instance._warnings = false;
        }

        [Test]
        public void TestNextLevelNameAndSceneName() {
            string[] info2 = LevelReferenceSingleton.Instance.GetNextLevelNameAndSceneName("Scene1");
            Assert.AreEqual("Level2", info2[0]);
            Assert.AreEqual("Scene2", info2[1]);
        }

        [Test]
        public void TestNextLevelWrongScene() {
            string[] info5 = LevelReferenceSingleton.Instance.GetNextLevelNameAndSceneName("Scene4");
            Assert.IsNull(info5);
        }

        [Test]
        public void TestNextLevelFinalScene() {
            string[] info4 = LevelReferenceSingleton.Instance.GetNextLevelNameAndSceneName("Scene3");
            Assert.IsNull(info4);
        }

        [Test]
        public void TestCurrentSceneLevelName() {
            string name = LevelReferenceSingleton.Instance.GetCurrentSceneLevelName("Scene3");
            Assert.AreEqual("Level3", name);
        }

        [Test]
        public void TestWrongSceneName() {
            string name = LevelReferenceSingleton.Instance.GetCurrentSceneLevelName("scene3");
            Assert.IsNull(name);
        }

        [Test]
        public void TestCurrentSceneLevelStars() {
            int stars1 = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(11, "Scene1");
            int stars2 = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(20, "Scene2");
            int stars3 = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(3, "Scene3");

            Assert.AreEqual(1, stars1);
            Assert.AreEqual(2, stars2);
            Assert.AreEqual(3, stars3);
        }

        [Test]
        public void TestWrongSceneStars() {
            int stars1 = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(11, "scene1");

            Assert.AreEqual(0, stars1);
        }

        [Test]
        public void TestCurrentSceneLevelPriceLimits() {
            int[] limits1 = LevelReferenceSingleton.Instance.GetCurrentScenePriceLimits("Scene1");
            Assert.AreEqual(1, limits1[0]);
            Assert.AreEqual(10, limits1[1]);
        }

        [Test]
        public void TestWrongScenePriceLimits() {
            int[] limits1 = LevelReferenceSingleton.Instance.GetCurrentScenePriceLimits("scene1");
            Assert.IsNull(limits1);
        }

        [TearDown]
        public void TearDown() {
            LevelReferenceSingleton.DestroyInstance();
        }
    }
}
