using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;
using BuilderGame.Levels.FileManagement;

namespace BuilderGame.Levels {
    public class LevelFileAccessSingletonTest
    {
        private LevelInfoScriptableObject[] scriptableObjects;

        [SetUp]
        public void SetUp() {
            LevelInfoScriptableObject info1 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            LevelInfoScriptableObject info2 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            LevelInfoScriptableObject info3 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info1.LevelName = "Level1";
            info2.LevelName = "Level2";
            info3.LevelName = "Level3";
            scriptableObjects = new LevelInfoScriptableObject[] {info1, info2, info3};

            LevelFileAccessSingleton.Instance._test = true;
            LevelFileAccessSingleton.Instance.CreateFileIfNotExists(scriptableObjects);
        }

        [Test]
        public void TestDefaultLevelStars() {
            Assert.AreEqual(0, LevelFileAccessSingleton.Instance.GetLevelStars("Level1"));
            Assert.AreEqual(0, LevelFileAccessSingleton.Instance.GetLevelStars("Level2"));
            Assert.AreEqual(0, LevelFileAccessSingleton.Instance.GetLevelStars("Level3"));
        }

        [Test]
        public void TestDefaultLevelState() {
            Assert.AreEqual(LevelState.NotPassed, LevelFileAccessSingleton.Instance.GetLevelState("Level1"));
            Assert.AreEqual(LevelState.Blocked, LevelFileAccessSingleton.Instance.GetLevelState("Level2"));
            Assert.AreEqual(LevelState.Blocked, LevelFileAccessSingleton.Instance.GetLevelState("Level3"));
        }

        [Test]
        public void TestSetLevelStars() {
            LevelFileAccessSingleton.Instance.SetLevelStars("Level2", 2);
            Assert.AreEqual(2, LevelFileAccessSingleton.Instance.GetLevelStars("Level2"));
        }

        [Test]
        public void TestSetLevelState() {
            LevelFileAccessSingleton.Instance.SetLevelState("Level2", LevelState.Passed);
            Assert.AreEqual(LevelState.Passed, LevelFileAccessSingleton.Instance.GetLevelState("Level2"));
        }

        [Test]
        public void TestNewLevels() {
            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4};

            LevelFileAccessSingleton.Instance.CreateFileIfNotExists(newLevels);
            LevelFileAccessSingleton.Instance.SetLevelStars("Level4", 3);
            LevelFileAccessSingleton.Instance.SetLevelState("Level4", LevelState.NotPassed);

            Assert.AreEqual(3, LevelFileAccessSingleton.Instance.GetLevelStars("Level4"));
            Assert.AreEqual(LevelState.NotPassed, LevelFileAccessSingleton.Instance.GetLevelState("Level4"));
        }

        [Test]
        public void TestInfosMaintainedAfterAddingNewLevels() {
            LevelFileAccessSingleton.Instance.SetLevelStars("Level3", 3);
            LevelFileAccessSingleton.Instance.SetLevelState("Level3", LevelState.NotPassed);

            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4};
            LevelFileAccessSingleton.Instance.CreateFileIfNotExists(newLevels);

            Assert.AreEqual(3, LevelFileAccessSingleton.Instance.GetLevelStars("Level3"));
            Assert.AreEqual(LevelState.NotPassed, LevelFileAccessSingleton.Instance.GetLevelState("Level3"));
        }

        [Test]
        public void TestDefaultStatesForNewLevelsIfLastWasPassed() {
            LevelFileAccessSingleton.Instance.SetLevelState("Level3", LevelState.Passed);

            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject info5 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info5.LevelName = "Level5";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4, info5};
            LevelFileAccessSingleton.Instance.CreateFileIfNotExists(newLevels);

            Assert.AreEqual(LevelState.NotPassed, LevelFileAccessSingleton.Instance.GetLevelState("Level4"));
            Assert.AreEqual(LevelState.Blocked, LevelFileAccessSingleton.Instance.GetLevelState("Level5"));
        }

        [Test]
        public void TestDefaultStatesForNewLevelsIfLastWasNotPassed() {
            LevelFileAccessSingleton.Instance.SetLevelState("Level3", LevelState.NotPassed);

            LevelInfoScriptableObject info4 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info4.LevelName = "Level4";
            LevelInfoScriptableObject info5 = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            info5.LevelName = "Level5";
            LevelInfoScriptableObject[] newLevels = new LevelInfoScriptableObject[] {
                scriptableObjects[0], scriptableObjects[1], scriptableObjects[2], info4, info5};
            LevelFileAccessSingleton.Instance.CreateFileIfNotExists(newLevels);

            Assert.AreEqual(LevelState.Blocked, LevelFileAccessSingleton.Instance.GetLevelState("Level4"));
            Assert.AreEqual(LevelState.Blocked, LevelFileAccessSingleton.Instance.GetLevelState("Level5"));
        }

        [TearDown]
        public void TearDown() {
            LevelFileAccessSingleton.DestroyInstance();
        }
    }
}
