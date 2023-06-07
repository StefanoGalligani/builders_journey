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
        private LevelFileManagerSingleton _fileManager;
        
        private void Start() {
            _selectables = new List<LevelSelectable>();
            LevelReferenceSingleton.Instance.SetReferences(_levelInfos);
            _fileManager = LevelFileManagerSingleton.Instance;
            _fileManager.CreateFileIfNotExists(_levelInfos);

            foreach(LevelInfoScriptableObject levelInfo in _levelInfos) {
                LevelSelectable levelSelectable = Instantiate<LevelSelectable>(_levelSelectablePrefab, _contentRect);
                int stars = _fileManager.GetLevelStars(levelInfo.LevelName);
                LevelState state = _fileManager.GetLevelState(levelInfo.LevelName);

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