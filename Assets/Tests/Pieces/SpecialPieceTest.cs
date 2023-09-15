using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.BuildingPhase.Start;
using UnityEngine.InputSystem;

namespace BuilderGame.SpecialPieces {
    public class SpecialPieceTest {
        private GameObject obj;
        private SpecialPiece pieceObj;
        private GameObject startObj;
        private StartNotifier startNotifier;
        private SpecialPieceController pieceController;

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            obj.AddComponent<Rigidbody2D>();
            obj.AddComponent<MockSpecialPiece>();
            pieceObj = obj.GetComponent<SpecialPiece>();
            
            startObj = new GameObject();
            startObj.AddComponent<StartNotifier>();
            startNotifier = startObj.GetComponent<StartNotifier>();

            pieceObj.ActionNames = new string[]{};
            
            pieceObj.Start();
            pieceController = ((MockSpecialPiece)pieceObj).GetController();
        }

        [Test]
        public void TestGameobjectAndTransformCorrectlyPassed() {
            Assert.AreEqual(obj, ((MockSpecialPieceController)pieceController).GetGameObject());
            Assert.AreEqual(obj.transform, ((MockSpecialPieceController)pieceController).GetTransform());
        }

        [Test]
        public void TestControllerMethodsBeforeGameStart() {
            pieceObj.Update();
            pieceObj.FixedUpdate();
            pieceObj.OnActionExecuted(default);
            pieceObj.Interrupt();

            Assert.False(((MockSpecialPieceController)pieceController).StartCalled);
            Assert.False(((MockSpecialPieceController)pieceController).UpdateCalled);
            Assert.False(((MockSpecialPieceController)pieceController).FixedUpdateCalled);
            Assert.False(((MockSpecialPieceController)pieceController).ActionExecuted);
            Assert.False(((MockSpecialPieceController)pieceController).Interrupted);
        }

        [Test]
        public void TestControllerMethodsAfterGameStart() {
            startNotifier.CanStart = true;
            startNotifier.StartGame();

            pieceObj.Update();
            pieceObj.FixedUpdate();
            pieceObj.OnActionExecuted(default);
            pieceObj.Interrupt();

            Assert.True(((MockSpecialPieceController)pieceController).StartCalled);
            Assert.True(((MockSpecialPieceController)pieceController).UpdateCalled);
            Assert.True(((MockSpecialPieceController)pieceController).FixedUpdateCalled);
            Assert.True(((MockSpecialPieceController)pieceController).ActionExecuted);
            Assert.True(((MockSpecialPieceController)pieceController).Interrupted);
        }

        [TearDown]
        public void TearDown() {
            pieceController = null;
            GameObject.DestroyImmediate(pieceObj);
            GameObject.DestroyImmediate(obj);
            GameObject.DestroyImmediate(startNotifier);
            GameObject.DestroyImmediate(startObj);
        }
    }
    public class MockSpecialPiece : SpecialPiece {
        protected override void InitController()
        {
            _controller = new MockSpecialPieceController();
        }

        internal SpecialPieceController GetController() {
            return _controller;
        }
    }
    public class MockSpecialPieceController : SpecialPieceController {
        public bool StartCalled = false;
        public bool UpdateCalled = false;
        public bool FixedUpdateCalled = false;
        public bool ActionExecuted = false;
        public bool Interrupted = false;
        internal override void StartPiece() {
            StartCalled = true;
        }
        internal override void UpdatePiece() {
            UpdateCalled = true;
        }
        internal override void FixedUpdatePiece() {
            FixedUpdateCalled = true;
        }
        internal override void OnActionExecuted(InputAction.CallbackContext context) {
            ActionExecuted = true;
        }
        internal override void Interrupt() {
            Interrupted = true;
        }

        internal GameObject GetGameObject() {
            return gameObject;
        }

        internal Transform GetTransform() {
            return transform;
        }
    }
}
