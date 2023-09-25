using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Sounds {
    public class SfxSpawner : EffectSpawner {

        public void SpawnSound(int key, Vector3 position, float duration = 1) {
            GameObject instance = base.GetEffect(key);
            AudioSource audioSource = instance.GetComponent<AudioSource>();
            audioSource.volume = _settings.GetSfxVolume();
            instance.transform.position = position;
            audioSource.Play();
            StartCoroutine(DestroySound(key, instance, duration));
        }

        private IEnumerator DestroySound(int key, GameObject instance, float duration) {
            yield return new WaitForSecondsRealtime(duration);
            if (instance != null){
                base.Release(key, instance);
            }
        }
    }
}
