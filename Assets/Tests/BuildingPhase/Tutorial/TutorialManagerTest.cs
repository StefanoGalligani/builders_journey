using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialManagerTest
    {
        private GameObject managerObj;
        private TutorialManager tutorialManager;
        private GameObject[] panelObjs;
        private TutorialPanel[] panels;
        private GameObject[] objElements;
        private MockTutorialElement[] mockTutorialElements;
        
        [SetUp]
        public void SetUp() {            
            objElements = new GameObject[7];
            mockTutorialElements = new MockTutorialElement[7];
            for (int i=0; i<7; i++) {
                objElements[i] = new GameObject();
                mockTutorialElements[i] = objElements[i].AddComponent<MockTutorialElement>();
            }

            panelObjs = new GameObject[3];
            panels = new TutorialPanel[3];
            for (int i=0; i<3; i++) {
                panelObjs[i] = new GameObject();
                panels[i] = panelObjs[i].AddComponent<TutorialPanel>();
                panels[i].SetLists(
                    new MonoBehaviour[] {mockTutorialElements[i*2]}.ToList(),
                    new MonoBehaviour[] {mockTutorialElements[i*2 + 1]}.ToList());
            }
            
            managerObj = new GameObject();
            tutorialManager = managerObj.AddComponent<TutorialManager>();
            tutorialManager.SetPanels(panels);
            tutorialManager.SetLinkedElements(new MonoBehaviour[] {mockTutorialElements[6]}.ToList());
            
            PlayerPrefs.SetInt("TutorialEnabled", 1);
            PlayerPrefs.SetInt("CurrentTutorialEnabled", 1);
        }

        [Test]
        public void TestOnValidate()
        {
            List<MonoBehaviour> correctList = new MonoBehaviour[] {mockTutorialElements[0]}.ToList();
            List<MonoBehaviour> fullList = new MonoBehaviour[] {panels[0], mockTutorialElements[0]}.ToList();

            tutorialManager.SetLinkedElements(fullList);
            tutorialManager.OnValidate();

            Assert.AreEqual(correctList, tutorialManager.GetLinkedElements());
        }

        [Test]
        public void TestTutorialDisabled()
        {
            PlayerPrefs.SetInt("CurrentTutorialEnabled", 0);
            tutorialManager.Start();
            Assert.False(managerObj.activeSelf);
            Assert.True(mockTutorialElements[0].enabledInTutorial);
            Assert.True(mockTutorialElements[6].enabledInTutorial);
        }

        [Test]
        public void TestTutorialEnabled()
        {
            tutorialManager.Start();
            Assert.True(managerObj.activeSelf);

            Assert.True(panelObjs[0].activeSelf);
            Assert.False(panelObjs[1].activeSelf);
            Assert.False(panelObjs[2].activeSelf);

            Assert.True(mockTutorialElements[0].enabledInTutorial);
            Assert.False(mockTutorialElements[1].enabledInTutorial);
            Assert.False(mockTutorialElements[2].enabledInTutorial);
            Assert.True(mockTutorialElements[3].enabledInTutorial);
            Assert.False(mockTutorialElements[4].enabledInTutorial);
            Assert.True(mockTutorialElements[5].enabledInTutorial);
            Assert.False(mockTutorialElements[6].enabledInTutorial);
        }

        [Test]
        public void TestOnLastPanel()
        {
            tutorialManager.Start();
            tutorialManager.OnNextPanel();
            tutorialManager.OnNextPanel();
            Assert.True(managerObj.activeSelf);

            Assert.False(panelObjs[0].activeSelf);
            Assert.False(panelObjs[1].activeSelf);
            Assert.True(panelObjs[2].activeSelf);

            Assert.True(mockTutorialElements[0].enabledInTutorial);
            Assert.False(mockTutorialElements[1].enabledInTutorial);
            Assert.True(mockTutorialElements[2].enabledInTutorial);
            Assert.False(mockTutorialElements[3].enabledInTutorial);
            Assert.True(mockTutorialElements[4].enabledInTutorial);
            Assert.False(mockTutorialElements[5].enabledInTutorial);
            Assert.False(mockTutorialElements[6].enabledInTutorial);
        }

        [Test]
        public void TestAfterLastPanel()
        {
            tutorialManager.Start();
            tutorialManager.OnNextPanel();
            tutorialManager.OnNextPanel();
            tutorialManager.OnNextPanel();
            Assert.False(managerObj.activeSelf);

            Assert.True(mockTutorialElements[0].enabledInTutorial);
            Assert.False(mockTutorialElements[1].enabledInTutorial);
            Assert.True(mockTutorialElements[2].enabledInTutorial);
            Assert.False(mockTutorialElements[3].enabledInTutorial);
            Assert.True(mockTutorialElements[4].enabledInTutorial);
            Assert.False(mockTutorialElements[5].enabledInTutorial);
            Assert.True(mockTutorialElements[6].enabledInTutorial);
        }
        
        [TearDown]
        public void TearDown() {
            tutorialManager = null;
            GameObject.DestroyImmediate(managerObj);
            
            for (int i=0; i<6; i++) {
                mockTutorialElements[i] = null;
                GameObject.DestroyImmediate(objElements[i]);
            }
            objElements = null;
            mockTutorialElements = null;

            for (int i=0; i<3; i++) {
                panels[i] = null;
                GameObject.DestroyImmediate(panelObjs[i]);
            }
            panelObjs = null;
            panels = null;
        }
    }
}
