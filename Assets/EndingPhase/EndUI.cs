using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.Price;
using BuilderGame.Levels;
using BuilderGame.Levels.FileManagement;
using System;

[assembly: InternalsVisibleToAttribute("EndingPhaseTests")]
namespace BuilderGame.EndingPhase
{
    public class EndUI : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPanel;
        [SerializeField] private string _menuSceneName;
        internal string _currentLevelName;
        internal string _nextLevelName;
        internal string _nextLevelSceneName;
        internal int _totalPrice;
        internal LevelFileAccess _fileManager;
        internal LevelReference _levelReference;

        internal void Start()
        {
            if (_uiPanel) _uiPanel.SetActive(false);
            FindObjectOfType<EndNotifier>().GameEnd += OnEndLevel;
            _fileManager = FindObjectOfType<LevelFileAccess>();
            _levelReference = FindObjectOfType<LevelReference>();
        }

        private void OnEndLevel() {
            if (_uiPanel) _uiPanel.SetActive(true);
            RetrieveSceneInfos();
            _totalPrice = FindObjectOfType<TotalPriceInfo>().GetTotalPrice();
            UpdateStars();
            UpdateStates();
        }

        internal void RetrieveSceneInfos(string sceneName = null) {
            _currentLevelName = _levelReference.GetCurrentSceneLevelName(sceneName);
            string[] nextLevelInfos = _levelReference.GetNextLevelNameAndSceneName(sceneName);
            if (nextLevelInfos != null) {
                _nextLevelName = nextLevelInfos[0];
                _nextLevelSceneName = nextLevelInfos[1];
            }
        }

        internal void UpdateStars(string sceneName = null) {
            int previousStars = _fileManager.GetLevelStars(_currentLevelName);
            int newStars = _levelReference.GetCurrentSceneLevelStars(_totalPrice, sceneName);
            if (newStars > previousStars) {
                _fileManager.SetLevelStars(_currentLevelName, newStars);
            }
        }

        internal void UpdateStates() {
            LevelState previousState = _fileManager.GetLevelState(_currentLevelName);
            if (previousState != LevelState.Passed) {
                _fileManager.SetLevelState(_currentLevelName, Levels.LevelState.Passed);
                _fileManager.SetLevelState(_nextLevelName, Levels.LevelState.NotPassed);
            }
        }

        public void OnMenuButtonClick() {
            SceneManager.LoadScene(_menuSceneName);
        }
        public void OnRestartButtonClick() {
            PlayerPrefs.SetInt("CurrentTutorialEnabled", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void OnNextLevelButtonClick() {
            PlayerPrefs.SetInt("CurrentTutorialEnabled", 1);
            SceneManager.LoadScene(_nextLevelSceneName);
        }
    }
}
