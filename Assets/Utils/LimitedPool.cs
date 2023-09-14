using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

[assembly: InternalsVisibleToAttribute("UtilsTestsPlayMode")]
namespace BuilderGame.Utils {
    public class LimitedPool : IObjectPool<GameObject> {
        internal Queue<GameObject> _inactiveQueue;
        internal Queue<GameObject> _activeQueue;
        internal List<GameObject> _notToRelease;
        private GameObject _prefab;
        private int _maxCount;
        private bool _limitReleases;
        
        public int CountInactive => _inactiveQueue.Count;
        public int CountActive => _activeQueue.Count;

        public LimitedPool(GameObject prefab, int initialCount, int maxCount, bool limitReleases = false) {
            _inactiveQueue = new Queue<GameObject>();
            _activeQueue = new Queue<GameObject>();
            _notToRelease = new List<GameObject>();
            _prefab = prefab;
            _maxCount = maxCount;
            _limitReleases = limitReleases;

            for (int i=0; i<initialCount; i++) {
                GameObject g = GameObject.Instantiate(_prefab);
                _inactiveQueue.Enqueue(g);
                g.SetActive(false);
            }
        }

        public GameObject GetPrefab() {
            return _prefab;
        }

        public void Clear() {
            foreach(GameObject g in _inactiveQueue) GameObject.Destroy(g);
            foreach(GameObject g in _activeQueue) GameObject.Destroy(g);
            _inactiveQueue.Clear();
            _activeQueue.Clear();
        }

        public GameObject Get() {
            if (CountInactive > 0) { //If inactive elements are available, take from there
                GameObject g = _inactiveQueue.Dequeue();
                g.SetActive(true);
                _activeQueue.Enqueue(g);
                return g;
            } else {
                if (CountActive + CountInactive < _maxCount) { //If there aren't too many elements, create a new one
                    GameObject g = GameObject.Instantiate(_prefab);
                    _activeQueue.Enqueue(g);
                    return g;
                } else { //If there are too many elements, take from existing
                    GameObject g = _activeQueue.Dequeue();
                    _activeQueue.Enqueue(g);
                    if(_limitReleases) _notToRelease.Add(g);
                    return g;
                }
            }
        }

        public PooledObject<GameObject> Get(out GameObject v) {
            throw new System.NotImplementedException();
        }

        public void Release(GameObject element) {
            if (!_activeQueue.Contains(element)) return;
            if (_limitReleases && _notToRelease.Contains(element)) {
                _notToRelease.Remove(element);
                return;
            }

            _activeQueue = new Queue<GameObject>(_activeQueue.Where(x => x != element));
            _inactiveQueue.Enqueue(element);
            element.SetActive(false);
        }
    }
}
