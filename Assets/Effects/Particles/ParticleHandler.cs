using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Particles {
    public class ParticleHandler : EffectHandler {
        [SerializeField] private ParticleSystem _particle;

        public override void StartEffect() {
            FindObjectOfType<ParticlesSpawner>()?.SpawnParticle(_particle, transform.position - Vector3.forward/2, transform.rotation);
        }

        public override void StopEffect() {
            
        }
    }
}
