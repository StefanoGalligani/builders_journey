using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuilderGame.Pieces {
    public class RocketPieceController : SpecialPieceController {
        private float _force;
        private Rigidbody2D _rb;
        private bool _on = false;
        private Animator _animator;

        internal RocketPieceController(float force, Animator animator) {
            _force = force;
            _animator = animator;
        }

        internal override void StartPiece() {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        internal override void UpdatePiece()
        {
            if (_on) {
                _rb.AddForce(transform.right*_force);
            }
        }

        internal override void OnActionExecuted(InputAction.CallbackContext context) {
            if (context.ReadValue<float>() > 0.5) {
                _on = true;
            } else {
                _on = false;
            }
            _animator.SetBool("Active", _on);
        }
    }
}
