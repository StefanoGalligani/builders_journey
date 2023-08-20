using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;

namespace BuilderGame.Levels {
    public class LevelReferenceSingleton {
        private LevelInfoScriptableObject[] _levelInfos;
        private static LevelReferenceSingleton _instance;
        public static LevelReferenceSingleton Instance {get {return (_instance==null ? (_instance = new LevelReferenceSingleton()) : _instance);} private set{} }

        private LevelReferenceSingleton(){}

        public void SetReferences(LevelInfoScriptableObject[] levelInfos) {
            for (int i=0; i<levelInfos.Length; i++) {
                _levelInfos = levelInfos;
            }
        }

        public string GetCurrentSceneLevelName() {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<_levelInfos.Length; i++) {
                if (sceneName == _levelInfos[i].SceneName) {
                    return _levelInfos[i].LevelName;
                }
            }
            Debug.LogWarning("Tried to access level name from scene " + sceneName + " but it was not found");
            return null;
        }

        public string[] GetNextLevelNameAndSceneName() {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<_levelInfos.Length-1; i++) {
                if (sceneName == _levelInfos[i].SceneName) {
                    return new string[]{_levelInfos[i+1].LevelName, _levelInfos[i+1].SceneName};
                }
            }
            Debug.LogWarning("Tried to access level scene after " + sceneName + " but it was not found");
            return null;
        }

        public int GetCurrentSceneLevelStars(int totalPrice) {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<_levelInfos.Length; i++) {
                if (sceneName == _levelInfos[i].SceneName) {
                    if (totalPrice <= _levelInfos[i].PriceLimitThreeStars) return 3;
                    if (totalPrice <= _levelInfos[i].PriceLimitTwoStars) return 2;
                    return 1;
                }
            }
            Debug.LogWarning("Tried to access level stars from scene " + sceneName + " but it was not found");
            return 0;
        }

        public int[] GetCurrentScenePriceLimits() {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<_levelInfos.Length-1; i++) {
                if (sceneName == _levelInfos[i].SceneName) {
                    return new int[]{_levelInfos[i].PriceLimitThreeStars, _levelInfos[i].PriceLimitTwoStars};
                }
            }
            Debug.LogWarning("Tried to access level prices from scene " + sceneName + " but it was not found");
            return null;
        }
    }
}
