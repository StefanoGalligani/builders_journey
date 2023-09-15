using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.SpecialPieces {
    [RequireComponent(typeof(Animator))]
    public class PropellerPiece : SpecialPiece {
        [SerializeField] private float _force;
        [SerializeField] private EffectContainer _effects;

        protected override void InitController()
        {
            _controller = new PropellerPieceController(_force, GetComponent<Animator>(), _effects);
        }
    }
}
