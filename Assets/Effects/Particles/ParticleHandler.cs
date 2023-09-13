using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Settings;

namespace BuilderGame.Effects.Particles {
    internal class ParticleHandler : EffectHandler {
        [SerializeField] private ParticleSystem _particle;
        private SettingsFileAccess _settings;
        private bool _on;

        private void Start() {
            _settings = FindObjectOfType<SettingsFileAccess>();
            _on = _settings.GetParticlesOn();
            _settings.SettingsUpdated += data => _on = data.ParticlesOn;
        }

        internal override void StartEffect() {
            if(!_on) return;
            FindObjectOfType<ParticlesSpawner>()?.SpawnParticle(_particle, transform.position - Vector3.forward/2, transform.rotation);
        }

        internal override void StopEffect() {
            
        }
    }
}
