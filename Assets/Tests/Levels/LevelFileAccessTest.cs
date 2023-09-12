using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;
using BuilderGame.Levels.FileManagement;

namespace BuilderGame.Levels {
    public class LevelFileAccessTest {
        private LevelInfoScriptableObject[] scriptableObjects;
        private GameObject obj;
        private LevelFileAccess levelFileAccess;

        [SetUp]
        public void SetUp() {
            LevelInfoScriptableObject info1 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            LevelInfoScriptableObject info2 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            LevelInfoScriptableObject info3 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info1.LevelName = "Level1";
            info2.LevelName = "Level2";
            info3.LevelName = "Level3";
            scriptableObjects = new LevelInfoScriptableObject[] {info1, info2, info3};

            obj = new GameObject();
            levelFileAccess = obj.AddComponent<LevelFileAccess>();
            levelFileAccess._test = true;
            levelFileAccess.CreateFileIfNotExists(scriptableObjects);
        }

        [Test]
        public void TestDefaultLevelStars() {
            Assert.AreEqual(0, levelFileAccess.GetLevelStars("Level1"));
            Assert.AreEqual(0, levelFileAccess.GetLevelStars("Level2"));
            Assert.AreEqual(0, levelFileAccess.GetLevelStars("Level3"));
        }

        [Test]
        public void TestDefaultLevelState() {
            Assert.AreEqual(LevelState.NotPassed, levelFileAccess.GetLevelState("Level1"));
            Assert.AreEqual(LevelState.Blocked, levelFileAccess.GetLevelState("Level2"));
            Assert.AreEqual(LevelState.Blocked, levelFileAccess.GetLevelState("Level3"));
        }

        [Test]
        public void TestSetLevelStars() {
            levelFileAccess.SetLevelStars("Level2", 2);
            Assert.AreEqual(2, levelFileAccess.GetLevelStars("Level2"));
        }

        [Test]
        public void TestSetLevelState() {
            levelFileAccess.SetLevelState("Level2", LevelState.Passed);
            Assert.AreEqual(LevelState.Passed, levelFileAccess.GetLevelState("Level2"));
        }

        [Test]
        public void TestNewLevels() {
            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4};

            levelFileAccess.CreateFileIfNotExists(newLevels);
            levelFileAccess.SetLevelStars("Level4", 3);
            levelFileAccess.SetLevelState("Level4", LevelState.NotPassed);

            Assert.AreEqual(3, levelFileAccess.GetLevelStars("Level4"));
            Assert.AreEqual(LevelState.NotPassed, levelFileAccess.GetLevelState("Level4"));
        }

        [Test]
        public void TestInfosMaintainedAfterAddingNewLevels() {
            levelFileAccess.SetLevelStars("Level3", 3);
            levelFileAccess.SetLevelState("Level3", LevelState.NotPassed);

            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4};
            levelFileAccess.CreateFileIfNotExists(newLevels);

            Assert.AreEqual(3, levelFileAccess.GetLevelStars("Level3"));
            Assert.AreEqual(LevelState.NotPassed, levelFileAccess.GetLevelState("Level3"));
        }

        [Test]
        public void TestDefaultStatesForNewLevelsIfLastWasPassed() {
            levelFileAccess.SetLevelState("Level3", LevelState.Passed);

            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject info5 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info5.LevelName = "Level5";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4, info5};
            levelFileAccess.CreateFileIfNotExists(newLevels);

            Assert.AreEqual(LevelState.NotPassed, levelFileAccess.GetLevelState("Level4"));
            Assert.AreEqual(LevelState.Blocked, levelFileAccess.GetLevelState("Level5"));
        }

        [Test]
        public void TestDefaultStatesForNewLevelsIfLastWasNotPassed() {
            levelFileAccess.SetLevelState("Level3", LevelState.NotPassed);

            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject info5 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info5.LevelName = "Level5";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4, info5};
            levelFileAccess.CreateFileIfNotExists(newLevels);

            Assert.AreEqual(LevelState.Blocked, levelFileAccess.GetLevelState("Level4"));
            Assert.AreEqual(LevelState.Blocked, levelFileAccess.GetLevelState("Level5"));
        }

        [TearDown]
        public void TearDown() {
            levelFileAccess = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
