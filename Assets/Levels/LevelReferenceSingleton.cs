using System;
using System.Collections.Generic;
using System.Linq;
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

        private int GetCurrentLevelIndex() {
            string sceneName = SceneManager.GetActiveScene().name;
            List<int> indices = _levelInfos.AsEnumerable().Select((l,i) => l.SceneName==sceneName ? i : -1).Except(new int[] {-1}).ToList();
            if (indices.Count > 0) {
                return indices.First();
            }
            Debug.LogWarning("Tried to access level from scene " + sceneName + " but it was not found");
            return -1;
        }

        private T GetCurrentLevelInfo<T>(Func<LevelInfoScriptableObject, T> action) {
            int index = GetCurrentLevelIndex();
            if (index >= 0) {
                return action(_levelInfos[index]);
            }
            return default;
        }

        public string[] GetNextLevelNameAndSceneName() {
            int index = GetCurrentLevelIndex();
            if (index >= 0 && index < _levelInfos.Length-1) {
                return new string[]{_levelInfos[index+1].LevelName, _levelInfos[index+1].SceneName};
            }
            Debug.LogWarning("No level found after this scene");
            return null;
        }

        public string GetCurrentSceneLevelName() {
            return GetCurrentLevelInfo(l => l.LevelName);
        }

        public int GetCurrentSceneLevelStars(int totalPrice) {
            return GetCurrentLevelInfo(l => {
                    if (totalPrice <= l.PriceLimitThreeStars) return 3;
                    if (totalPrice <= l.PriceLimitTwoStars) return 2;
                    return 1;
                });
        }

        public int[] GetCurrentScenePriceLimits() {
            return GetCurrentLevelInfo(l => new int[]{l.PriceLimitThreeStars, l.PriceLimitTwoStars});
        }
    }
}
