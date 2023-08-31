using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialPanelTest
    {
        private GameObject panelObj;
        private TutorialPanel panel;
        private GameObject objElement;
        private MockTutorialElement mockTutorialElement;
        
        [SetUp]
        public void SetUp() {
            panelObj = new GameObject();
            panel = panelObj.AddComponent<TutorialPanel>();
            objElement = new GameObject();
            mockTutorialElement = objElement.AddComponent<MockTutorialElement>();
        }

        [Test]
        public void TestOnValidate()
        {
            List<MonoBehaviour> correctList = new MonoBehaviour[] {mockTutorialElement}.ToList();
            List<MonoBehaviour> fullList1 = new MonoBehaviour[] {panel, mockTutorialElement}.ToList();
            List<MonoBehaviour> fullList2 = new MonoBehaviour[] {mockTutorialElement, panel}.ToList();
            panel.SetLists(fullList1, fullList2);

            panel.OnValidate();

            Assert.AreEqual(correctList, panel.GetLinkedElements());
            Assert.AreEqual(correctList, panel.GetDeactivatedElements());
        }
        
        [TearDown]
        public void TearDown() {
            panel = null;
            mockTutorialElement = null;
            GameObject.DestroyImmediate(panelObj);
            GameObject.DestroyImmediate(objElement);
        }
    }

    public class MockTutorialElement : MonoBehaviour, ITutorialElement
    {
        public bool enabledInTutorial = true;
        void ITutorialElement.DisableInTutorial()
        {
            enabledInTutorial = false;
        }

        void ITutorialElement.EnableInTutorial()
        {
            enabledInTutorial = true;
        }
    }
}
