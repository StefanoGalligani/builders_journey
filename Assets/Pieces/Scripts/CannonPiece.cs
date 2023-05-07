using UnityEngine;

namespace BuilderGame.Pieces {
    public class CannonPiece : SpecialPiece {
        [SerializeField] private Rigidbody2D _cannonBall;
        [SerializeField] private float _force;

        protected override void InitController()
        {
            _controller = new CannonPieceController(_cannonBall, _force);
        }
    }
}