using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Particles {
    public class ParticlesSpawner : EffectSpawner {
        public void SpawnParticle(int key, Vector3 position, Quaternion rotation, float duration = 1) {
            GameObject instance = base.GetEffect(key);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            ParticleSystem ps = instance.GetComponent<ParticleSystem>();
            ps.time = 0;
            StartCoroutine(DestroyParticle(key, instance, duration));
        }

        private IEnumerator DestroyParticle(int key, GameObject instance, float duration) {
            yield return new WaitForSeconds(duration);
            if (instance != null)
                base.Release(key, instance);
        }
    }
}
