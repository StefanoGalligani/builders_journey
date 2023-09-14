using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects {
    internal abstract class EffectHandler : MonoBehaviour {
        private EffectSpawner _spawner;
        private void Start() {
            _spawner = SetSpawner();
            if (_spawner != null) {
                int key = _spawner.AddEffect(GetEffectPrefab());
                SetEffectKey(key);
            }
            StartHandler();
        }
        protected abstract EffectSpawner SetSpawner();
        protected abstract GameObject GetEffectPrefab();
        protected abstract void SetEffectKey(int key);
        protected abstract void StartHandler();
        internal abstract void StartEffect();
        internal abstract void StopEffect();
    }
}
