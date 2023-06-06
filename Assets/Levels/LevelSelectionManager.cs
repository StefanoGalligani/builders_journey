using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.Levels.FileManagement;

namespace BuilderGame.Levels {
    public class LevelSelectionManager : MonoBehaviour {
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private LevelSelectable _levelSelectablePrefab;
        [SerializeField] private LevelInfoScriptableObject[] _levelInfos;
        private List<LevelSelectable> _selectables;
        
        private void Start() {
            _selectables = new List<LevelSelectable>();
            LevelFileManagerSingleton.Instance.CreateFileIfNotExists(_levelInfos);

            foreach(LevelInfoScriptableObject levelInfo in _levelInfos) {
                LevelSelectable levelSelectable = Instantiate<LevelSelectable>(_levelSelectablePrefab, _contentRect);
                int stars = LevelFileManagerSingleton.Instance.GetLevelStars(levelInfo.LevelName);
                LevelState state = LevelFileManagerSingleton.Instance.GetLevelState(levelInfo.LevelName);

                levelSelectable.Init(levelInfo, stars, state, this);
                _selectables.Add(levelSelectable);
            }
        }

        private void OnGameStart() {
            gameObject.SetActive(false);
        }

        public void Selection(LevelSelectable levelSelectable, string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}