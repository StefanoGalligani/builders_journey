using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.Utils {
    public class UtilsFunctionsTest
    {
        [Test]
        public void TestIsValidPosition()
        {
            Object[][] matrix30 = new Object[][] {new Object[0], new Object[0], new Object[0]};
            Object[][] matrix34 = new Object[][] {new Object[4], new Object[4], new Object[4]};

            Assert.False(UtilsFunctions.IsValidPosition(matrix30, 0, 0));

            Assert.True(UtilsFunctions.IsValidPosition(matrix34, 0, 0));
            Assert.True(UtilsFunctions.IsValidPosition(matrix34, 2, 3));

            Assert.False(UtilsFunctions.IsValidPosition(matrix34, -1, 0));
            Assert.False(UtilsFunctions.IsValidPosition(matrix34, 0, -1));
            Assert.False(UtilsFunctions.IsValidPosition(matrix34, 3, 3));
            Assert.False(UtilsFunctions.IsValidPosition(matrix34, 2, 4));
        }
    }
}
