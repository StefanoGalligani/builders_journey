using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.Utils;
using BuilderGame.Levels;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;

namespace BuilderGame.MainMenu.LevelSelection {
    public class LevelSelectableTest
    {
        private LevelSelectable selectable;
        private LevelInfoScriptableObject info;
        private MockSelectionUI selectionUI;
        private GameObject obj;

        [SetUp]
        public void SetUp() {
            selectionUI = new MockSelectionUI();
            info = ScriptableObject.CreateInstance<LevelInfoScriptableObject>();
            obj = new GameObject();
            obj.AddComponent<LevelSelectable>();
            selectable = obj.GetComponent<LevelSelectable>();
        }
        
        [Test]
        public void TestOnClickWhenNotClickable()
        {
            selectable.Init(info, 0, LevelState.Blocked, selectionUI);
            selectable.OnClick();
            Assert.False(selectionUI.selected);
        }
        
        [Test]
        public void TestOnClickWhenClickable()
        {
            selectable.Init(info, 0, LevelState.NotPassed, selectionUI);
            selectable.OnClick();
            Assert.True(selectionUI.selected);
        }
        
        [Test]
        public void TestOnClickWhenPassed()
        {
            selectable.Init(info, 0, LevelState.Passed, selectionUI);
            selectable.OnClick();
            Assert.True(selectionUI.selected);
        }

        [TearDown]
        public void TearDown() {
            selectionUI = null;
            GameObject.DestroyImmediate(selectable);
            GameObject.DestroyImmediate(obj);
        }
    }
    public class MockSelectionUI : ISelectionUI<LevelSelectable, LevelInfoScriptableObject> {
        public bool selected = false;
        public void Selection(LevelSelectable levelSelectable, LevelInfoScriptableObject info) {
            selected = true;
        }
    }
}
