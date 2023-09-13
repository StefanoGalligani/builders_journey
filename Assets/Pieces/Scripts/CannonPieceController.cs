using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BuilderGame.Effects;

namespace BuilderGame.Pieces {
    public class CannonPieceController : SpecialPieceController {
        private float _force;
        private Rigidbody2D _rb;
        private EffectContainer _effects;
        private CannonBallPool _pool;

        internal CannonPieceController(float force, EffectContainer effects) {
            _force = force;
            _effects = effects;
        }

        internal override void StartPiece() {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _pool = GameObject.FindObjectOfType<CannonBallPool>();
        }

        internal override void OnActionExecuted(InputAction.CallbackContext context) {
            if (context.ReadValue<float>() > 0.5) {
                Rigidbody2D cb = _pool.Get();
                cb.position = transform.position + transform.right;
                cb.velocity = Vector2.zero;
                
                cb.AddForce(transform.right * _force, ForceMode2D.Impulse);
                _rb.AddForce(-transform.right * _force, ForceMode2D.Impulse);
                _effects.StartEffects();
            }
        }

        internal override void Interrupt() {
            
        }
    }
}