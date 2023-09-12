using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Sounds {
    public class SfxSpawner : MonoBehaviour {

        public void SpawnSound(AudioSource sound, Vector3 position, float duration = 1) {
            AudioSource instance = Instantiate(sound, position, Quaternion.identity);
            instance.Play();
            StartCoroutine(DestroySound(instance.gameObject, duration));
        }

        private IEnumerator DestroySound(GameObject instance, float duration) {
            yield return new WaitForSecondsRealtime(duration);
            if (instance != null){
                Destroy(instance);
            }
        }
    }
}
