using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Particles {
    public class ParticlesSpawner : MonoBehaviour {
        public void SpawnParticle(ParticleSystem particle, Vector3 position, Quaternion rotation, float duration = 1) {
            ParticleSystem instance = Instantiate(particle, position, rotation);
            StartCoroutine(DestroyParticle(instance, duration));
        }

        private IEnumerator DestroyParticle(ParticleSystem particle, float duration) {
            yield return new WaitForSeconds(duration);
            if (particle != null)
                Destroy(particle.gameObject);
        }
    }
}
