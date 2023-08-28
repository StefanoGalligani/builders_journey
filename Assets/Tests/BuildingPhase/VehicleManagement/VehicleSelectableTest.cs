using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    public class VehicleSelectableTest
    {
        private VehicleSelectable selectable;
        private VehicleInfo info;
        private MockVehicleSelectionUI selectionUI;
        private GameObject obj;

        [SetUp]
        public void SetUp() {
            selectionUI = new MockVehicleSelectionUI();
            info = new VehicleInfo("name");
            obj = new GameObject();
            selectable = obj.AddComponent<VehicleSelectable>();
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

    public class MockVehicleSelectionUI : ISelectionUI<VehicleSelectable, VehicleInfo> {
        public bool selected = false;
        public void Selection(VehicleSelectable pieceSelectable, VehicleInfo info) {
            selected = true;
        }
    }
}
