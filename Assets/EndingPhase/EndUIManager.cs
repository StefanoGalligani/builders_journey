using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.Price;
using BuilderGame.Levels;
using BuilderGame.Levels.FileManagement;

namespace BuilderGame.EndingPhase
{
    public class EndUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPanel;
        [SerializeField] private string _menuSceneName;
        private string _nextLevelSceneName;
        private void Start()
        {
            _uiPanel.SetActive(false);
            FindObjectOfType<EndNotifier>().GameEnd += OnEndLevel;
        }

        private void OnEndLevel() {
            _uiPanel.SetActive(true);
            string currentLevelName = LevelReferenceSingleton.Instance.GetCurrentSceneLevelName();
            string[] nextLevelInfos = LevelReferenceSingleton.Instance.GetNextLevelNameAndSceneName();
            _nextLevelSceneName = nextLevelInfos[1];
            Levels.LevelState previousState = LevelFileManagerSingleton.Instance.GetLevelState(currentLevelName);
            int previousStars = LevelFileManagerSingleton.Instance.GetLevelStars(currentLevelName);

            int totalPrice = FindObjectOfType<TotalPriceInfo>().GetTotalPrice();
            int newStars = LevelReferenceSingleton.Instance.GetCurrentSceneLevelStars(totalPrice);
            if (newStars > previousStars) {
                LevelFileManagerSingleton.Instance.SetLevelStars(currentLevelName, newStars);
            }

            if (previousState != LevelState.Passed) {
                LevelFileManagerSingleton.Instance.SetLevelState(currentLevelName, Levels.LevelState.Passed);
                LevelFileManagerSingleton.Instance.SetLevelState(nextLevelInfos[0], Levels.LevelState.NotPassed);
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
