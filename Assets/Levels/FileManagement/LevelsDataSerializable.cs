using System;

namespace BuilderGame.Levels.FileManagement
{
    [Serializable]
    internal class LevelsDataSerializable
    {
        public int levelCount;
        public SingleLevelData[] data;
    }
}
