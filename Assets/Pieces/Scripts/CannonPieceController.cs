using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using BuilderGame.Effects;

namespace BuilderGame.Pieces {
    public class CannonPieceController : SpecialPieceController {
        private Rigidbody2D _cannonBall;
        private float _force;
        private Rigidbody2D _rb;
        private EffectContainer _effects;

        internal CannonPieceController(Rigidbody2D cannonBall, float force, EffectContainer effects) {
            _cannonBall = cannonBall;
            _force = force;
            _effects = effects;
        }

        internal override void StartPiece() {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        internal override void OnActionExecuted(InputAction.CallbackContext context) {
            if (context.ReadValue<float>() > 0.5) {
                Rigidbody2D cb = GameObject.Instantiate(_cannonBall, transform.position + transform.right, Quaternion.identity);

                cb.AddForce(transform.right * _force, ForceMode2D.Impulse);
                _rb.AddForce(-transform.right * _force, ForceMode2D.Impulse);
                _effects.StartEffects();
            }
        }

        internal override void Interrupt() {
            
        }
    }
}