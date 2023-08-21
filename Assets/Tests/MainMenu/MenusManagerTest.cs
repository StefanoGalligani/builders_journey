using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.MainMenu {
    public class MenusManagerTest
    {
        private GameObject[] menus;
        private GameObject obj;
        private MenusManager menusManager;

        [SetUp]
        public void SetUp() {
            menus = new GameObject[] {new GameObject(), new GameObject(), new GameObject()};
            obj = new GameObject();
            obj.AddComponent<MenusManager>();
            foreach (GameObject g in menus) {
                g.transform.SetParent(obj.transform);
            }
            menusManager = obj.GetComponent<MenusManager>();
            menusManager.Start();
        }
        
        [Test]
        public void TestInitialCondition()
        {
            Assert.False(menus[0].activeSelf);
            Assert.False(menus[1].activeSelf);
            Assert.False(menus[2].activeSelf);
        }
        
        [Test]
        public void TestOpenMenu()
        {
            menusManager.OpenMenu(1);
            Assert.False(menus[0].activeSelf);
            Assert.True(menus[1].activeSelf);
            Assert.False(menus[2].activeSelf);
            
            menusManager.OpenMenu(0);
            Assert.True(menus[0].activeSelf);
            Assert.False(menus[1].activeSelf);
            Assert.False(menus[2].activeSelf);
        }

        [TearDown]
        public void TearDown() {
            menusManager = null;
            GameObject.DestroyImmediate(obj);
            GameObject.DestroyImmediate(menus[0]);
            GameObject.DestroyImmediate(menus[1]);
            GameObject.DestroyImmediate(menus[2]);
            menus = null;
        }
    }
}
