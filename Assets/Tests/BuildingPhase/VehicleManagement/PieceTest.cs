using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.VehicleManagement {
    public class PieceTest {
        GameObject obj;
        Piece pieceObj;

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            pieceObj = obj.AddComponent<Piece>();
            pieceObj._availableJointDirections = new DirectionEnum[] {DirectionEnum.Down, DirectionEnum.Right};
            pieceObj.Init(1, new Vector2Int(3, 4), Vector3.zero);
        }

        [Test]
        public void TestShift() {
            pieceObj.Shift(Direction.Right, 1);
            Assert.AreEqual(Vector3.right, obj.transform.position);
            Assert.AreEqual(new Vector2Int(4, 4), pieceObj.GridPosition);
        }

        [Test]
        public void TestRotateWhenCantRotate() {
            pieceObj.Rotate();
            Assert.AreEqual(new Direction(Direction.Right), pieceObj.FacingDirection);
        }

        [Test]
        public void TestRotateWithoutJoint() {
            pieceObj._canRotate = true;
            pieceObj.Rotate();
            Assert.AreEqual(new Direction(Direction.Up), pieceObj.FacingDirection);
        }

        [Test]
        public void TestRotateWithJoint() {
            pieceObj._canRotate = true;
            pieceObj.ConnectJoint(null, Direction.Right);
            pieceObj.Rotate();
            Assert.AreEqual(new Direction(Direction.Up), pieceObj.FacingDirection);
            pieceObj.Rotate();
            Assert.AreEqual(new Direction(Direction.Right), pieceObj.FacingDirection);
        }

        [Test]
        public void TestConnectJoint() {
            pieceObj._canRotate = true;
            pieceObj.ConnectJoint(null, Direction.Left);
            Assert.AreEqual(new Direction(Direction.Left), pieceObj.FacingDirection);
        }

        [Test]
        public void TestIsPossibleJointDirectionWhenCanRotate() {
            pieceObj._canRotate = true;
            Assert.True(pieceObj.IsPossibleJointDirection(Direction.Right));
            Assert.True(pieceObj.IsPossibleJointDirection(Direction.Up));
            Assert.True(pieceObj.IsPossibleJointDirection(Direction.Left));
            Assert.True(pieceObj.IsPossibleJointDirection(Direction.Down));
        }

        [Test]
        public void TestIsPossibleJointDirectionWhenCantRotate() {
            Assert.True(pieceObj.IsPossibleJointDirection(Direction.Right));
            Assert.True(pieceObj.IsPossibleJointDirection(Direction.Down));
            Assert.False(pieceObj.IsPossibleJointDirection(Direction.Up));
            Assert.False(pieceObj.IsPossibleJointDirection(Direction.Left));
        }

        [Test]
        public void TestIsAvailableJointDirection() {
            pieceObj._canRotate = true;
            pieceObj.Rotate();
            Assert.True(pieceObj.IsAvailableJointDirection(Direction.Right));
            Assert.True(pieceObj.IsAvailableJointDirection(Direction.Up));
            Assert.False(pieceObj.IsAvailableJointDirection(Direction.Down));
            Assert.False(pieceObj.IsAvailableJointDirection(Direction.Left));
        }

        [TearDown]
        public void TearDown() {
            pieceObj = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}