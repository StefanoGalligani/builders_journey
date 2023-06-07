using UnityEngine;

namespace BuilderGame.Levels {
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelInfoScriptableObject", order = 1)]
    public class LevelInfoScriptableObject : ScriptableObject
    {
        public string LevelName;
        public string SceneName;
    }
}