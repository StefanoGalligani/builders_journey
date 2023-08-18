using UnityEngine;
using UnityEngine.InputSystem;

namespace BuilderGame.Pieces {
    public class RocketPieceController : SpecialPieceController {
        private float _force;
        private Rigidbody2D _rb;
        private bool _wasActive = false;
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
            if (Keyboard.current.spaceKey.isPressed) {
                _rb.AddForce(transform.right*_force);
                SetState(true);
            } else {
                SetState(false);
            }
        }

        private void SetState(bool active) {
            if (active != _wasActive) {
                _wasActive = active;
                _animator.SetBool("Active", active);
            }
        }
    }
}
