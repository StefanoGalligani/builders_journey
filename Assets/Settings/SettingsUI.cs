using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderGame.Settings {
    public class SettingsUI : MonoBehaviour {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _audioSlider;
        [SerializeField] private Slider _sensitivitySlider;
        [SerializeField] private Toggle _tooltipsToggle;
        [SerializeField] private Toggle _particlesToggle;
        private SettingsFileAccess _settingsFileAccess;

        private void Start() {
            _settingsFileAccess = FindObjectOfType<SettingsFileAccess>();
            _musicSlider.value = _settingsFileAccess.GetMusicVolume();
            _audioSlider.value = _settingsFileAccess.GetSfxVolume();
            _sensitivitySlider.value = _settingsFileAccess.GetCameraSensitivity();
            _tooltipsToggle.isOn = _settingsFileAccess.GetTooltipsOn();
            _particlesToggle.isOn = _settingsFileAccess.GetParticlesOn();

            _musicSlider.onValueChanged.AddListener(_ => OnSettingsChanged());
            _audioSlider.onValueChanged.AddListener(_ => OnSettingsChanged());
            _sensitivitySlider.onValueChanged.AddListener(_ => OnSettingsChanged());
            _tooltipsToggle.onValueChanged.AddListener(_ => OnSettingsChanged());
            _particlesToggle.onValueChanged.AddListener(_ => OnSettingsChanged());
        }

        private void OnSettingsChanged() {
            _settingsFileAccess.UpdateMusicVolume(_musicSlider.value);
            _settingsFileAccess.UpdateSfxVolume(_audioSlider.value);
            _settingsFileAccess.UpdateCameraSensitivity(_sensitivitySlider.value);
            _settingsFileAccess.UpdateTooltipsOn(_tooltipsToggle.isOn);
            _settingsFileAccess.UpdateParticlesOn(_particlesToggle.isOn);
        }

    }
}
