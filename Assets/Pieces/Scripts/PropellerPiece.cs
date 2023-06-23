using UnityEngine;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Animator))]
    public class PropellerPiece : SpecialPiece {
        [SerializeField] private float _force;

        protected override void InitController()
        {
            _controller = new PropellerPieceController(_force, GetComponent<Animator>());
        }
    }
}
