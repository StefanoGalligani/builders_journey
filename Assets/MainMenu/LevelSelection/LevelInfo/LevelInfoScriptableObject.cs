using UnityEngine;

namespace BuilderGame.MainMenu.LevelSelection.LevelInfo {
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelInfoScriptableObject", order = 1)]
    public class LevelInfoScriptableObject : ScriptableObject
    {
        public string LevelName;
        public string SceneName;
        public int PriceLimitThreeStars;
        public int PriceLimitTwoStars;
    }
}