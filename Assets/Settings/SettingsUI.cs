using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using BuilderGame.Input;

namespace BuilderGame.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private bool _isMainMenu = false;
        [SerializeField] private string _menuSceneName;
        private Controls _actionAsset;
        private bool _isContentOpen = false;

        private void Start() {
            Time.timeScale = 1;
            _content.SetActive(_isMainMenu);
            _isContentOpen = _isMainMenu;
            _actionAsset = new Controls();
            _actionAsset.Enable();
            _actionAsset.defaultmap.Pause.performed += ctx => OnPause();
        }

        private void OnPause()
        {
            ToggleContent(!_isContentOpen);
        }

        public void OnCloseSettings() {
            if (_isMainMenu) return;
            ToggleContent(false);
        }

        public void OnLoadMenu() {
            if (_isMainMenu) return;
            SceneManager.LoadScene(_menuSceneName);
        }

        public void OnRestart() {
            if (_isMainMenu) return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ToggleContent(bool value) {
            _content.SetActive(value);
            _isContentOpen = value;
            Time.timeScale = value ? 0 : 1;
        }

        private void OnDisable() {
            _actionAsset.Disable();
        }
    }
}
