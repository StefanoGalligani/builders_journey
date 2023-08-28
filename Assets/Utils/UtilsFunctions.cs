using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Utils {
    public static class UtilsFunctions
    {
        public static bool IsValidPosition(Object[][] matrix, int x, int y) {
            if (x < 0 || x >= matrix.Length)
                return false;
            if (y < 0 || y >= matrix[x].Length)
                return false;
            return true;
        }
    }
}