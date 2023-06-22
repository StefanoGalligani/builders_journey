using UnityEngine;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Animator))]
    public class RocketPiece : SpecialPiece {
        [SerializeField] private float _force;

        protected override void InitController()
        {
            _controller = new RocketPieceController(_force, GetComponent<Animator>());
        }
    }
}
