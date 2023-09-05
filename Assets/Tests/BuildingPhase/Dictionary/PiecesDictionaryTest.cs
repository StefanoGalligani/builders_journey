using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.BuildingPhase.PieceSelection.PieceInfo;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.BuildingPhase.Dictionary {
    public class PiecesDictionaryTest
    {
        private GameObject dictionaryObject;
        private PiecesDictionary dictionary;
        private PieceInfoScriptableObject info1;
        private PieceInfoScriptableObject info2;
        private PieceInfoScriptableObject info3;
        private GameObject mainPrefab;
        private GameObject prefab1;
        private GameObject prefab2;
        private GameObject prefab3;
        private Piece mainPiece;
        private Piece piece1;
        private Piece piece2;
        private Piece piece3;

        [SetUp]
        public void SetUp() {
            dictionaryObject = new GameObject();
            dictionary = dictionaryObject.AddComponent<PiecesDictionary>();

            mainPrefab = new GameObject();
            mainPiece = mainPrefab.AddComponent<Piece>();
            mainPrefab.AddComponent<SpriteRenderer>();

            prefab1 = new GameObject();
            prefab2 = new GameObject();
            prefab3 = new GameObject();
            piece1 = mainPrefab.AddComponent<Piece>();
            piece2 = mainPrefab.AddComponent<Piece>();
            piece3 = mainPrefab.AddComponent<Piece>();

            info1 = ScriptableObject.CreateInstance<PieceInfoScriptableObject>();
            info2 = ScriptableObject.CreateInstance<PieceInfoScriptableObject>();
            info3 = ScriptableObject.CreateInstance<PieceInfoScriptableObject>();
            info1.Id = 1;
            info2.Id = 2;
            info3.Id = 3;
            info1.Price = 10;
            info2.Price = 20;
            info3.Price = 30;
            info1.Prefab = prefab1;
            info2.Prefab = prefab2;
            info3.Prefab = prefab3;

            dictionary._mainPiecePrefab = mainPrefab;
            dictionary.Init(new PieceInfoScriptableObject[] {info1, info2, info3});
        }
        
        [Test]
        public void TestGetPrefabById() {
            GameObject prefab = dictionary.GetPrefabById(0);
            Assert.AreEqual(mainPrefab, prefab);
            prefab = dictionary.GetPrefabById(1);
            Assert.AreEqual(prefab1, prefab);
            prefab = dictionary.GetPrefabById(4);
            Assert.AreEqual(null, prefab);
        }
        
        [Test]
        public void TestGetPriceById() {
            int price = dictionary.GetPriceById(0);
            Assert.AreEqual(0, price);
            price = dictionary.GetPriceById(2);
            Assert.AreEqual(20, price);
            price = dictionary.GetPriceById(4);
            Assert.AreEqual(0, price);
        }
        
        [Test]
        public void TestIdsValid() {
            int[] ids = new int[] {0, 1, 2, 3, 2, 1, 0};
            Assert.True(dictionary.AreAllIdsValid(ids));
        }
        
        [Test]
        public void TestIdsNotValid() {
            int[] ids = new int[] {0, 1, 2, 3, 2, 1, 0, 4};
            Assert.False(dictionary.AreAllIdsValid(ids));
        }

        [TearDown]
        public void TearDown() {
            dictionary = null;
            GameObject.DestroyImmediate(dictionaryObject);

            mainPiece = null;
            piece1 = null;
            piece2 = null;
            piece3 = null;
            GameObject.DestroyImmediate(mainPrefab);
            GameObject.DestroyImmediate(prefab1);
            GameObject.DestroyImmediate(prefab2);
            GameObject.DestroyImmediate(prefab3);
        }
    }
}
