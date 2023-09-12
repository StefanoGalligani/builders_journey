using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Settings;

namespace BuilderGame.Effects.Sounds {
    public class SfxHandler : EffectHandler {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private bool _isSoundPrefab;
        private SettingsFileAccess _settings;

        private void Start() {
            _settings = FindObjectOfType<SettingsFileAccess>();
            _sound.volume = _settings.GetSfxVolume();
            _settings.SettingsUpdated += UpdateSoundVolume;
        }

        public override void StartEffect() {
            if (_settings.GetSfxVolume() == 0) return;
            if (_isSoundPrefab) {
                FindObjectOfType<SfxSpawner>()?.SpawnSound(_sound, transform.position - Vector3.forward);
            } else {
                _sound.Play();
            }
        }
        
        public override void StopEffect() {
            if (!_isSoundPrefab) {
                _sound.Stop();
            }
        }

        private void UpdateSoundVolume(SettingsDataSerializable settingsData) {
            _sound.volume = settingsData.SfxVolume;
        }
    }
}
