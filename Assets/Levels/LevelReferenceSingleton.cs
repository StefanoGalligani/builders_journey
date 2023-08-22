using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("LevelsTests")]
[assembly: InternalsVisibleToAttribute("EndingPhaseTests")]
namespace BuilderGame.Levels {
    public class LevelReferenceSingleton {
        private LevelInfoScriptableObject[] _levelInfos;
        private static LevelReferenceSingleton _instance;
        internal bool _warnings = true;
        public static LevelReferenceSingleton Instance {get {return (_instance==null ? (_instance = new LevelReferenceSingleton()) : _instance);} private set{_instance = value;} }
        private LevelReferenceSingleton(){}

        public void SetReferences(LevelInfoScriptableObject[] levelInfos) {
            for (int i=0; i<levelInfos.Length; i++) {
                _levelInfos = levelInfos;
            }
        }

        private int GetCurrentLevelIndex(string sceneName = null) {
            if(sceneName==null) sceneName = SceneManager.GetActiveScene().name;
            List<int> indices = _levelInfos.AsEnumerable().Select((l,i) => l.SceneName==sceneName ? i : -1).Except(new int[] {-1}).ToList();
            if (indices.Count > 0) {
                return indices.First();
            }
            if(_warnings) Debug.LogWarning("Tried to access level from scene " + sceneName + " but it was not found");
            return -1;
        }

        private T GetCurrentLevelInfo<T>(Func<LevelInfoScriptableObject, T> action, string sceneName = null) {
            int index = GetCurrentLevelIndex(sceneName);
            if (index >= 0) {
                return action(_levelInfos[index]);
            }
            return default;
        }

        public string[] GetNextLevelNameAndSceneName(string sceneName = null) {
            int index = GetCurrentLevelIndex(sceneName);
            if (index >= 0 && index < _levelInfos.Length-1) {
                return new string[]{_levelInfos[index+1].LevelName, _levelInfos[index+1].SceneName};
            }
            if(_warnings) Debug.LogWarning("No level found after this scene");
            return null;
        }

        public string GetCurrentSceneLevelName(string sceneName = null) {
            return GetCurrentLevelInfo(l => l.LevelName, sceneName);
        }

        public int GetCurrentSceneLevelStars(int totalPrice, string sceneName = null) {
            return GetCurrentLevelInfo(l => {
                    if (totalPrice <= l.PriceLimitThreeStars) return 3;
                    if (totalPrice <= l.PriceLimitTwoStars) return 2;
                    return 1;
                }, sceneName);
        }

        public int[] GetCurrentScenePriceLimits(string sceneName = null) {
            return GetCurrentLevelInfo(l => new int[]{l.PriceLimitThreeStars, l.PriceLimitTwoStars}, sceneName);
        }

        internal static void DestroyInstance() {
            Instance = null;
        }
    }
}
