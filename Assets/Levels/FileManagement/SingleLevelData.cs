using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Levels.FileManagement
{
    [Serializable]
    internal struct SingleLevelData
    {
        public string levelName;
        public int levelStars;
        public LevelState levelState;
    }
}
