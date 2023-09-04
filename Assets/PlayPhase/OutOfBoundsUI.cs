using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.Start;

namespace BuilderGame.PlayPhase
{
    public class OutOfBoundsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPanel;

        internal void Start()
        {
            if (_uiPanel) _uiPanel.SetActive(false);
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void OnGameStart() {
            FindObjectOfType<OutOfBoundsNotifier>().OutOfBounds += OnOutOfBounds;
        }

        private void OnOutOfBounds() {
            if (_uiPanel) _uiPanel.SetActive(true);
        }

        public void OnRestartButtonClick() {
            //PlayerPrefs.SetInt("CurrentTutorialEnabled", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
