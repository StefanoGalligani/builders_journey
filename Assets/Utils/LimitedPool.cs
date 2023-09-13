using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace BuilderGame.Utils {
    public class LimitedPool : IObjectPool<GameObject> {
        private Queue<GameObject> _inactiveQueue;
        private Queue<GameObject> _activeQueue;
        private GameObject _prefab;
        private int _maxCount;
        
        public int CountInactive => _inactiveQueue.Count;
        public int CountActive => _activeQueue.Count;

        public LimitedPool(GameObject prefab, int initialCount, int maxCount) {
            _inactiveQueue = new Queue<GameObject>();
            _activeQueue = new Queue<GameObject>();
            _prefab = prefab;
            _maxCount = maxCount;

            for (int i=0; i<initialCount; i++) {
                GameObject g = GameObject.Instantiate(_prefab);
                _inactiveQueue.Enqueue(g);
                g.SetActive(false);
            }
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
                    return g;
                }
            }
        }

        public PooledObject<GameObject> Get(out GameObject v) {
            throw new System.NotImplementedException();
        }

        public void Release(GameObject element) {
            if (!_activeQueue.Contains(element)) return;

            _activeQueue = new Queue<GameObject>(_activeQueue.Where(x => x != element));
            _inactiveQueue.Append(element);
            element.SetActive(false);
        }
    }
}
