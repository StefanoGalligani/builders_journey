using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.Utils
{
    public class DirectionTest
    {
        [Test]
        public void TestEquals()
        {
            Direction dir = new Direction(Direction.Up);

            Assert.False(dir.Equals(new object()));
            Assert.False(dir.Equals(Direction.Right));
            Assert.False(dir.Equals(DirectionEnum.Left));
            Assert.False(dir.Equals(new Direction(4)));
            
            Assert.True(dir.Equals(Direction.Up));
            Assert.True(dir.Equals(DirectionEnum.Up));
            Assert.True(dir.Equals(new Direction(Direction.Up)));
        }

        [Test]
        public void TestImplicitOperatorsFromDirection()
        {
            Direction dir = new Direction(Direction.Up);
            Direction dirNull = new Direction(Direction.Null);

            Assert.AreEqual(1, (int)dir);
            Assert.AreEqual(new Vector2(0,1), (Vector2)dir);
            Assert.AreEqual(new Vector3(0,1,0), (Vector3)dir);
            Assert.AreEqual(new Vector2Int(0,1), (Vector2Int)dir);
            Assert.AreEqual(new Vector3Int(0,1,0), (Vector3Int)dir);
            Assert.AreEqual(true, (bool)dir);
            Assert.AreEqual(false, (bool)dirNull);
        }

        [Test]
        public void TestImplicitOperatorsToDirection()
        {
            Direction r = new Direction(Direction.Right);
            Direction u = new Direction(Direction.Up);
            Direction l = new Direction(Direction.Left);
            Direction d = new Direction(Direction.Down);
            Direction dirNull = new Direction(Direction.Null);

            Assert.AreEqual(r, (Direction)new Vector2(3,2));
            Assert.AreEqual(r, (Direction)new Vector2(3,3));
            Assert.AreEqual(r, (Direction)new Vector2(3,-3));
            Assert.AreEqual(l, (Direction)new Vector2(-3,-3));
            Assert.AreEqual(u, (Direction)new Vector2(-2,3));
            Assert.AreEqual(d, (Direction)new Vector2(2,-3));
            Assert.AreEqual(dirNull, (Direction)new Vector2(0, 0));
        }

        [Test]
        public void TestArithmetics()
        {
            Direction right = new Direction(Direction.Right);
            Direction up = new Direction(Direction.Up);
            Direction left = new Direction(Direction.Left);
            Direction down = new Direction(Direction.Down);
            Direction nullDir = new Direction(Direction.Null);

            Assert.AreEqual(right + right, right);
            Assert.AreEqual(right + up, up);
            Assert.AreEqual(up + up, left);
            Assert.AreEqual(up + left, down);
            Assert.AreEqual(left + left, right);
            Assert.AreEqual(left + down, up);
            Assert.AreEqual(down + down, left);
            Assert.AreEqual(down + right, down);
            Assert.AreEqual(up + nullDir, nullDir);

            Assert.AreEqual(right - right, right);
            Assert.AreEqual(up - right, up);
            Assert.AreEqual(up - up, right);
            Assert.AreEqual(up - left, down);
            Assert.AreEqual(left - up, up);
            Assert.AreEqual(right-2, left);
            Assert.AreEqual(down-2, up);
            Assert.AreEqual(up - nullDir, nullDir);
        }
    }
}
