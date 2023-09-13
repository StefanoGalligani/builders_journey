using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.Pieces {
    public class CannonPiece : SpecialPiece {
        [SerializeField] private float _force;
        [SerializeField] private EffectContainer _effects;

        protected override void InitController()
        {
            _controller = new CannonPieceController(_force, _effects);
        }
    }
}