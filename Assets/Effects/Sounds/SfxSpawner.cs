using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Sounds {
    public class SfxSpawner : MonoBehaviour {

        public void SpawnSound(AudioSource sound, Vector3 position, float duration = 1) {
            AudioSource instance = Instantiate(sound, position, Quaternion.identity);
            instance.Play();
            StartCoroutine(DestroySound(instance, duration));
        }

        private IEnumerator DestroySound(AudioSource sound, float duration) {
            yield return new WaitForSeconds(duration);
            if (sound != null)
                Destroy(sound.gameObject);
        }
    }
}
