using UnityEngine;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(HingeJoint2D))]
    public class HingePiece : SpecialPiece {
        [SerializeField] private int _speed;
        
        protected override void InitController()
        {
            _controller = new HingePieceController(_speed);
        }
    }
}