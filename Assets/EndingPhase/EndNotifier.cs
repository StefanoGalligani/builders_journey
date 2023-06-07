using UnityEngine;
using System;

namespace BuilderGame.EndingPhase
{
    public class EndNotifier : MonoBehaviour
    {
        public event Action GameEnd;
        private bool _activated = false;
        private void OnTriggerEnter2D(Collider2D other) {
            if (_activated || !other.gameObject.layer.Equals(LayerMask.NameToLayer("Vehicle"))) return;

            _activated = true;
            GameEnd?.Invoke();
        }
    }
}
