using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Particles {
    public class ParticleHandler : EffectHandler {
        [SerializeField] ParticleSystem _particle;

        public override void StartEffect() {
            FindObjectOfType<ParticlesSpawner>()?.SpawnParticle(_particle, transform.position - Vector3.forward, transform.rotation);
        }
    }
}
