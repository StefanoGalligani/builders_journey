using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.EndingPhase;

namespace BuilderGame.PlayPhase
{
    public class OutOfBoundsNotifier : MonoBehaviour
    {
        public event Action OutOfBounds;
        [SerializeField] private float _yLimit;
        private bool _raised;

        private void Start() {
            _raised = false;
            FindObjectOfType<EndNotifier>().GameEnd += OnGameEnd;
        }

        private void Update() {
            if (transform.position.y <= _yLimit && !_raised) {
                _raised = true;
                OutOfBounds?.Invoke();
            }
        }

        private void OnGameEnd() {
            _raised = true;
        }
    }
}
