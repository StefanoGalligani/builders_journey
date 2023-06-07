using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuilderGame.Levels {
    public class LevelReferenceSingleton {
        private string[] levelNames;
        private string[] levelSceneNames;
        private static LevelReferenceSingleton _instance;
        public static LevelReferenceSingleton Instance {get {return (_instance==null ? (_instance = new LevelReferenceSingleton()) : _instance);} private set{} }

        private LevelReferenceSingleton(){}

        public void SetReferences(LevelInfoScriptableObject[] levelInfos) {
            levelNames = new string[levelInfos.Length];
            levelSceneNames = new string[levelInfos.Length];
            for (int i=0; i<levelInfos.Length; i++) {
                levelNames[i] = levelInfos[i].LevelName;
                levelSceneNames[i] = levelInfos[i].SceneName;
            }
        }

        public string GetCurrentSceneLevelName() {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<levelSceneNames.Length; i++) {
                if (sceneName == levelSceneNames[i]) {
                    return levelNames[i];
                }
            }
            Debug.LogWarning("Tried to access level name from scene " + sceneName + " but it was not found");
            return null;
        }

        public string[] GetNextLevelNameAndSceneName() {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<levelSceneNames.Length-1; i++) {
                if (sceneName == levelSceneNames[i]) {
                    return new string[]{levelNames[i+1], levelSceneNames[i+1]};
                }
            }
            Debug.LogWarning("Tried to access level scene after " + sceneName + " but it was not found");
            return null;
        }
    }
}
