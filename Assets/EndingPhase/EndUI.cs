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

        internal void Start()
        {
            if (_uiPanel) _uiPanel.SetActive(false);
            FindObjectOfType<EndNotifier>().GameEnd += OnEndLevel;
        }

        private void OnEndLevel() {
            if (_uiPanel) _uiPanel.SetActive(true);
            RetrieveSceneInfos();
            _totalPrice = FindObjectOfType<TotalPriceInfo>().GetTotalPrice();
            UpdateStars();
            UpdateStates();
        }

        internal void RetrieveSceneInfos(string sceneName = null) {
            _currentLevelName = LevelReferenceSingleton.Instance.GetCurrentSceneLevelName(sceneName);
            string[] nextLevelInfos = LevelReferenceSingleton.Instance.GetNextLevelNameAndSceneName(sceneName);
            _nextLevelName = nextLevelInfos[0];
            _nextLevelSceneName = nextLevelInfos[1];
        }

        internal void UpdateStars(string sceneName = null) {
            int previousStars = LevelFileAccessSingleton.Instance.GetLevelStars(_currentLevelName);
            int newStars = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(_totalPrice, sceneName);
            if (newStars > previousStars) {
                LevelFileAccessSingleton.Instance.SetLevelStars(_currentLevelName, newStars);
            }
        }

        internal void UpdateStates() {
            LevelState previousState = LevelFileAccessSingleton.Instance.GetLevelState(_currentLevelName);
            if (previousState != LevelState.Passed) {
                LevelFileAccessSingleton.Instance.SetLevelState(_currentLevelName, Levels.LevelState.Passed);
                LevelFileAccessSingleton.Instance.SetLevelState(_nextLevelName, Levels.LevelState.NotPassed);
            }
        }

        public void OnMenuButtonClick() {
            SceneManager.LoadScene(_menuSceneName);
        }
        public void OnNextLevelButtonClick() {
            SceneManager.LoadScene(_nextLevelSceneName);
        }
    }
}
