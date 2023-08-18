using UnityEngine;
using UnityEngine.InputSystem;

namespace BuilderGame.Pieces {
    public class CannonPieceController : SpecialPieceController {
        private Rigidbody2D _cannonBall;
        private float _force;
        private Rigidbody2D _rb;

        internal CannonPieceController(Rigidbody2D cannonBall, float force) {
            _cannonBall = cannonBall;
            _force = force;
        }

        internal override void StartPiece() {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        internal override void UpdatePiece()
        {
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                Rigidbody2D cb = GameObject.Instantiate(_cannonBall, transform.position + transform.right, Quaternion.identity);
                cb.AddForce(transform.right * _force, ForceMode2D.Impulse);
                _rb.AddForce(-transform.right * _force, ForceMode2D.Impulse);
            }
        }
    }
}