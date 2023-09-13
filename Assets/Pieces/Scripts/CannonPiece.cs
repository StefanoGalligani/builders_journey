using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.Pieces {
    public class CannonPiece : SpecialPiece {
        [SerializeField] private Rigidbody2D _cannonBall;
        [SerializeField] private float _force;
        [SerializeField] private EffectContainer _effects;

        protected override void InitController()
        {
            _controller = new CannonPieceController(_cannonBall, _force, _effects);
        }
    }
}