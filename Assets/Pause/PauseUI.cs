using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using BuilderGame.Input;
using UnityEngine.SceneManagement;

[assembly: InternalsVisibleToAttribute("PauseTests")]
namespace BuilderGame.Pause {
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] internal GameObject _content;
        [SerializeField] internal GameObject _settings;
        [SerializeField] internal string _menuSceneName;
        private Controls _actionAsset;
        private bool _isContentOpen;

        internal void Start() {
            Time.timeScale = 1;
            if (_content) _content.SetActive(false);
            if (_settings) _settings.SetActive(false);
            _isContentOpen = false;
            _actionAsset = new Controls();
            _actionAsset.Enable();
            _actionAsset.defaultmap.Pause.performed += ctx => OnPause();
        }

        public void OnPause()
        {
            _isContentOpen = !_isContentOpen;
            if (_settings) _settings.SetActive(false);
            if (_content) _content.SetActive(_isContentOpen);
            Time.timeScale = _isContentOpen ? 0 : 1;
        }

        public void OnToggleSettings(bool on) {
            if (!_isContentOpen) return;
            if (_settings) _settings.SetActive(on);
        }

        public void OnLoadMenu() {
            SceneManager.LoadScene(_menuSceneName);
        }

        public void OnRestart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDisable() {
            _actionAsset.Disable();
        }
    }
}
