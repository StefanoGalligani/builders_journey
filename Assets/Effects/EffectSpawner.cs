using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.Utils;
using UnityEngine.Events;
using System.Linq;

namespace BuilderGame.Effects {
    public abstract class EffectSpawner : MonoBehaviour {
        [SerializeField] private int _initialPoolSize;
        [SerializeField] private int _maxPoolSize;
        private Dictionary<int, LimitedPool> _poolsDictionary;

        private void Awake() {
            _poolsDictionary = new Dictionary<int, LimitedPool>();
            SceneManager.sceneLoaded += OnSceneChange;
        }

        internal int AddEffect(GameObject prefab) {
            int id = prefab.GetInstanceID();
            if (!_poolsDictionary.ContainsKey(id)) {
                LimitedPool pool = new LimitedPool(prefab, _initialPoolSize, _maxPoolSize, true);
                _poolsDictionary.Add(id, pool);
            }
            return id;
        }

        protected GameObject GetEffect(int key) {
            if (!_poolsDictionary.ContainsKey(key)) {
                Debug.LogError("GameObject " + key + " not found in dictionary");
                return null;
            }
            return _poolsDictionary.GetValueOrDefault(key).Get();
        }

        protected void Release(int key, GameObject instance) {
            if (!_poolsDictionary.ContainsKey(key)) {
                Debug.LogError("GameObject " + key + " not found in dictionary");
                return;
            }
            _poolsDictionary.GetValueOrDefault(key).Release(instance);
        }

        private void OnSceneChange(Scene scene, LoadSceneMode mode) {
            foreach(KeyValuePair<int, LimitedPool> poolPair in _poolsDictionary.ToArray()) {
                int key = poolPair.Key;
                _poolsDictionary.Remove(key);
            }
        }
    }
}
