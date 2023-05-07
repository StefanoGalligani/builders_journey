using UnityEngine;

namespace BuilderGame.Pieces {
    public class RocketPieceController : SpecialPieceController {
        private float _force;
        private Rigidbody2D _rb;

        internal RocketPieceController(float force) {
            _force = force;
        }

        internal override void StartPiece() {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        internal override void UpdatePiece()
        {
            if (Input.GetKey(KeyCode.Space)) {
                _rb.AddForce(transform.right*_force);
            }
        }
    }
}
