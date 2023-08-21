using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.Price;
using BuilderGame.Levels;
using BuilderGame.Levels.FileManagement;

[assembly: InternalsVisibleToAttribute("EndingPhaseTests")]
namespace BuilderGame.EndingPhase
{
    public class EndUI : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPanel;
        [SerializeField] private string _menuSceneName;
        private string _currentLevelName;
        private string _nextLevelName;
        private string _nextLevelSceneName;
        private Levels.LevelState _previousState;
        private int _previousStars;

        internal void Start()
        {
            if (_uiPanel) _uiPanel.SetActive(false);
            FindObjectOfType<EndNotifier>().GameEnd += OnEndLevel;
        }

        private void OnEndLevel() {
            if (_uiPanel) _uiPanel.SetActive(true);
            RetrieveSceneInfos();
            RetrieveOldInfos();
            UpdateStars();
            UpdateStates();
        }

        private void RetrieveSceneInfos() {
            _currentLevelName = LevelReferenceSingleton.Instance.GetCurrentSceneLevelName();
            string[] nextLevelInfos = LevelReferenceSingleton.Instance.GetNextLevelNameAndSceneName();
            _nextLevelName = nextLevelInfos[0];
            _nextLevelSceneName = nextLevelInfos[1];
        }

        private void RetrieveOldInfos() {
            _previousState = LevelFileAccessSingleton.Instance.GetLevelState(_currentLevelName);
            _previousStars = LevelFileAccessSingleton.Instance.GetLevelStars(_currentLevelName);
        }

        private void UpdateStars() {
            int totalPrice = FindObjectOfType<TotalPriceInfo>().GetTotalPrice();
            int newStars = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(totalPrice);
            if (newStars > _previousStars) {
                LevelFileAccessSingleton.Instance.SetLevelStars(_currentLevelName, newStars);
            }
        }

        private void UpdateStates() {
            if (_previousState != LevelState.Passed) {
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
