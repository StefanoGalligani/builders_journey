using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.Levels;
using BuilderGame.Utils;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;
using BuilderGame.Levels.FileManagement;

namespace BuilderGame.MainMenu.LevelSelection {
    public class LevelSelectionUI : MonoBehaviour, ISelectionUI<LevelSelectable, LevelInfoScriptableObject> {
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private LevelSelectable _levelSelectablePrefab;
        [SerializeField] private LevelInfoScriptableObject[] _levelInfos;
        private List<LevelSelectable> _selectables;
        private LevelFileAccess _fileManager;
        
        private void Start() {
            _selectables = new List<LevelSelectable>();
            LevelReferenceSingleton.Instance.SetReferences(_levelInfos);
            _fileManager = FindObjectOfType<LevelFileAccess>();
            _fileManager.CreateFileIfNotExists(_levelInfos);

            foreach(LevelInfoScriptableObject levelInfo in _levelInfos) {
                LevelSelectable levelSelectable = Instantiate<LevelSelectable>(_levelSelectablePrefab, _contentRect);
                int stars = _fileManager.GetLevelStars(levelInfo.LevelName);
                LevelState state = _fileManager.GetLevelState(levelInfo.LevelName);

                levelSelectable.Init(levelInfo, stars, state, this);
                _selectables.Add(levelSelectable);
            }
        }

        public void Selection(LevelSelectable levelSelectable, LevelInfoScriptableObject levelInfo) {
            SceneManager.LoadScene(levelInfo.SceneName);
        }
    }
}