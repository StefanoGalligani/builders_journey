using System;

namespace BuilderGame.Levels.FileManagement
{
    [Serializable]
    internal class LevelsDataSerializable
    {
        public int levelCount;
        public string[] levelsNames;
        public int[] levelsStars;
        public LevelState[] levelsStates;
    }
}
