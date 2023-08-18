using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace BuilderGame
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private bool _isMainMenu = false;
        [SerializeField] private string _menuSceneName;
        private bool _isContentOpen = false;

        private void Start() {
            Time.timeScale = 1;
            _content.SetActive(_isMainMenu);
            _isContentOpen = _isMainMenu;
        }

        private void Update()
        {
            //cambiare con Action
            if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                ToggleContent(!_isContentOpen);
            }
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
    }
}
