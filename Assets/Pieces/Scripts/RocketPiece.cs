using UnityEngine;

namespace BuilderGame.Pieces {
    public class RocketPiece : SpecialPiece {
        [SerializeField] private float _force;

        protected override void InitController()
        {
            _controller = new RocketPieceController(_force);
        }
    }
}
