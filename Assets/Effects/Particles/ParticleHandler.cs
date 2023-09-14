using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Settings;

namespace BuilderGame.Effects.Particles {
    internal class ParticleHandler : EffectHandler {
        [SerializeField] private ParticleSystem _particle;
        private ParticlesSpawner _particleSpawner;
        private SettingsFileAccess _settings;
        private bool _on;
        private int _key;
        private float _duration;

        protected override void StartHandler() {
            _settings = FindObjectOfType<SettingsFileAccess>();
            _on = _settings.GetParticlesOn();
            _settings.SettingsUpdated += data => _on = data.ParticlesOn;

            _duration = _particle.main.duration;
        }

        protected override EffectSpawner SetSpawner() {
            _particleSpawner = FindObjectOfType<ParticlesSpawner>();
            return _particleSpawner;
        }

        protected override GameObject GetEffectPrefab() {
            return _particle.gameObject;
        }

        protected override void SetEffectKey(int key) {
            _key = key;
        }

        internal override void StartEffect() {
            if(!_on) return;
            _particleSpawner?.SpawnParticle(_key, transform.position - Vector3.forward/2, transform.rotation, _duration);
        }

        internal override void StopEffect() {
            
        }
    }
}
