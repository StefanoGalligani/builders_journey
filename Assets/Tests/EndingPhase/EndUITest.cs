using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.Levels;
using BuilderGame.Levels.FileManagement;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;

namespace BuilderGame.EndingPhase {
    public class EndUITest
    {
        private LevelInfoScriptableObject[] scriptableObjects;
        private GameObject obj;
        private EndUI endUI;
        private LevelFileAccess levelFileAccess;

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            endUI = obj.AddComponent<EndUI>();
            LevelReferenceSingleton.DestroyInstance();

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

            levelFileAccess = obj.AddComponent<LevelFileAccess>();
            levelFileAccess._test = true;
            levelFileAccess.CreateFileIfNotExists(scriptableObjects);
            endUI._fileManager = levelFileAccess;

            LevelReferenceSingleton.Instance.SetReferences(scriptableObjects);
            LevelReferenceSingleton.Instance._warnings = false;
        }
        
        [Test]
        public void TestRetrieveSceneInfos() {
            endUI.RetrieveSceneInfos("Scene1");

            Assert.AreEqual("Level1", endUI._currentLevelName);
            Assert.AreEqual("Level2", endUI._nextLevelName);
            Assert.AreEqual("Scene2", endUI._nextLevelSceneName);
        }
        
        [Test]
        public void TestUpdateStars() {
            endUI._currentLevelName = "Level2";
            endUI._totalPrice = 20;

            endUI.UpdateStars("Scene2");

            Assert.AreEqual(2, levelFileAccess.GetLevelStars("Level2"));
        }
        
        [Test]
        public void TestDontUpdateStarsWhenLower() {
            levelFileAccess.SetLevelStars("Level2", 3);
            endUI._currentLevelName = "Level2";
            endUI._totalPrice = 20;

            endUI.UpdateStars("Scene2");

            Assert.AreEqual(3, levelFileAccess.GetLevelStars("Level2"));
        }
        
        [Test]
        public void TestUpdateStatesWhenUnlocking() {
            levelFileAccess.SetLevelState("Level2", LevelState.NotPassed);
            endUI._currentLevelName = "Level2";
            endUI._nextLevelName = "Level3";

            endUI.UpdateStates();

            Assert.AreEqual(Levels.LevelState.Passed, levelFileAccess.GetLevelState("Level2"));
            Assert.AreEqual(Levels.LevelState.NotPassed, levelFileAccess.GetLevelState("Level3"));
        }

        [Test]
        public void TestUpdateStatesWhenAlreadyPassed() {
            levelFileAccess.SetLevelState("Level2", LevelState.Passed);
            levelFileAccess.SetLevelState("Level3", LevelState.Passed);
            endUI._currentLevelName = "Level2";
            endUI._nextLevelName = "Level3";

            endUI.UpdateStates();

            Assert.AreEqual(Levels.LevelState.Passed, levelFileAccess.GetLevelState("Level3"));
        }

        [TearDown]
        public void TearDown() {
            LevelReferenceSingleton.DestroyInstance();
            levelFileAccess = null;
            endUI = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
