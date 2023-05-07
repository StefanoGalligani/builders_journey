using UnityEngine;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(WheelJoint2D))]
    public class WheelPiece : SpecialPiece {
        [SerializeField] private int _speed;

        protected override void InitController()
        {
            _controller = new WheelPieceController(_speed);
        }
    }
}