using System.Collections;
using System.Collections.Generic;
using BuilderGame.Utils;
using UnityEngine;

namespace BuilderGame.SpecialPieces {
    public class CannonBallPool : MonoBehaviour {
        [SerializeField] Rigidbody2D _prefab;
        [SerializeField] int _initialPoolSize;
        [SerializeField] int _maxPoolSize;
        private LimitedPool _pool;

        private void Start() {
            _pool = new LimitedPool(_prefab.gameObject, _initialPoolSize, _maxPoolSize);
        }

        public Rigidbody2D Get() {
            return _pool.Get().GetComponent<Rigidbody2D>();
        }
    }
}
