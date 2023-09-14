using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Settings;

namespace BuilderGame.Effects.Sounds {
    internal class SfxHandler : EffectHandler {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private bool _isSoundPrefab;
        private SfxSpawner _sfxSpawner;
        private SettingsFileAccess _settings;
        private int _key;
        private float _duration;

        protected override void StartHandler() {
            _settings = FindObjectOfType<SettingsFileAccess>();
            _sound.volume = _settings.GetSfxVolume();
            _settings.SettingsUpdated += data => _sound.volume = data.SfxVolume;
            _duration = _sound.clip.length;
        }

        protected override EffectSpawner SetSpawner() {
            if (_isSoundPrefab) {
                _sfxSpawner = FindObjectOfType<SfxSpawner>();
                return _sfxSpawner;
            }
            return null;
        }

        protected override GameObject GetEffectPrefab() {
            return _sound.gameObject;
        }

        protected override void SetEffectKey(int key) {
            _key = key;
        }

        internal override void StartEffect() {
            if (_sound.volume == 0) return;
            if (_isSoundPrefab) {
                _sfxSpawner?.SpawnSound(_key, transform.position - Vector3.forward, _duration);
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
