using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.Utils;
using BuilderGame.BuildingPhase.PieceSelection.PieceInfo;

namespace BuilderGame.BuildingPhase.PieceSelection {
    public class PieceSelectableTest
    {
        private PieceSelectable selectable;
        private PieceInfoScriptableObject info;
        private MockPieceSelectionUI selectionUI;
        private GameObject obj;

        [SetUp]
        public void SetUp() {
            selectionUI = new MockPieceSelectionUI();
            info = new PieceInfoScriptableObject();
            obj = new GameObject();
            obj.AddComponent<PieceSelectable>();
            selectable = obj.GetComponent<PieceSelectable>();
        }
        
        [Test]
        public void TestOnClick()
        {
            selectable.Init(info, selectionUI);
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
    public class MockPieceSelectionUI : ISelectionUI<PieceSelectable, PieceInfoScriptableObject> {
        public bool selected = false;
        public void Selection(PieceSelectable pieceSelectable, PieceInfoScriptableObject info) {
            selected = true;
        }
    }
}
