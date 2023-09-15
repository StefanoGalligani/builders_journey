using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Settings;

namespace BuilderGame.Soundtrack {
    public class Soundtrack : MonoBehaviour {
        private SettingsFileAccess _settings;
        private AudioSource _audioSource;
        private void Start() {
            _audioSource = GetComponent<AudioSource>();
            _settings = FindObjectOfType<SettingsFileAccess>();
            _audioSource.volume = _settings.GetMusicVolume();
            _settings.SettingsUpdated += data => _audioSource.volume = data.MusicVolume;
        }
    }
}
