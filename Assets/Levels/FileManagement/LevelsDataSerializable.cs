using System;

namespace BuilderGame.Levels.FileManagement
{
    [Serializable]
    internal class LevelsDataSerializable
    {
        public string[] levelsNames;
        public int[] levelsStars;
        public LevelState[] levelsStates;
    }
}
