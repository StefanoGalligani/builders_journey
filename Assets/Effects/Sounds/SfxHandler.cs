using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects.Sounds {
    public class SfxHandler : EffectHandler {
        [SerializeField] private AudioSource _sound;
        [SerializeField] private bool _isSoundPrefab;

        public override void StartEffect() {
            if (_isSoundPrefab) {
                FindObjectOfType<SfxSpawner>()?.SpawnSound(_sound, transform.position - Vector3.forward);
            } else {
                _sound.Play();
            }
        }
        
        public override void StopEffect() {
            if (!_isSoundPrefab) {
                _sound.Stop();
            }
        }
    }
}
