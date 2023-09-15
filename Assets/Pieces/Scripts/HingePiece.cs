using UnityEngine;

namespace BuilderGame.SpecialPieces {
    [RequireComponent(typeof(HingeJoint2D))]
    public class HingePiece : SpecialPiece {
        [SerializeField] private int _speed;
        [SerializeField] private SpriteRenderer _baseSprite;
        
        protected override void InitController()
        {
            _controller = new HingePieceController(_speed, _baseSprite);
        }
    }
}