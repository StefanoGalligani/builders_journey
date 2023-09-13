using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Settings;

namespace BuilderGame.Effects.Sounds {
    internal class SfxHandler : EffectHandler {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private bool _isSoundPrefab;
        private SettingsFileAccess _settings;

        private void Start() {
            _settings = FindObjectOfType<SettingsFileAccess>();
            _sound.volume = _settings.GetSfxVolume();
            _settings.SettingsUpdated += data => _sound.volume = data.SfxVolume;
        }

        internal override void StartEffect() {
            if (_settings.GetSfxVolume() == 0) return;
            if (_isSoundPrefab) {
                FindObjectOfType<SfxSpawner>()?.SpawnSound(_sound, transform.position - Vector3.forward);
            } else {
                _sound.Play();
            }
        }
        
        internal override void StopEffect() {
            if (!_isSoundPrefab) {
                _sound.Stop();
            }
        }
    }
}
