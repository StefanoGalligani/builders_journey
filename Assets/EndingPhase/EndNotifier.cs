using UnityEngine;
using System;

namespace BuilderGame.EndingPhase
{
    public class EndNotifier : MonoBehaviour
    {
        public event Action GameEnd;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _activated = false;
        internal void OnTriggerEnter2D(Collider2D other) {
            if (_activated || !other.gameObject.layer.Equals(LayerMask.NameToLayer("Vehicle"))) return;

            _activated = true;
            GameEnd?.Invoke();
            if(_spriteRenderer) Destroy(_spriteRenderer);
        }
    }
}
