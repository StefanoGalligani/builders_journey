using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Input;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _settings;
    [SerializeField] private string _menuSceneName;
    private Controls _actionAsset;
    private bool _isContentOpen = false;

    private void Start() {
        Time.timeScale = 1;
        _content.SetActive(false);
        _actionAsset = new Controls();
        _actionAsset.Enable();
        _actionAsset.defaultmap.Pause.performed += ctx => OnPause();
    }

    public void OnPause()
    {
        _settings.SetActive(false);
        _isContentOpen = !_isContentOpen;
        _content.SetActive(_isContentOpen);
        Time.timeScale = _isContentOpen ? 0 : 1;
    }

    public void OnToggleSettings(bool on) {
        _settings.SetActive(on);
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
