using UnityEngine;

namespace BuilderGame.MainMenu {
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelInfoScriptableObject", order = 1)]
    public class LevelInfoScriptableObject : ScriptableObject
    {
        public string Name;
        public string SceneName;
    }
}