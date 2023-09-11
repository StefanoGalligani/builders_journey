using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Animator))]
    public class PropellerPiece : SpecialPiece {
        [SerializeField] private float _force;
        [SerializeField] private List<EffectHandler> _effects;

        protected override void InitController()
        {
            _controller = new PropellerPieceController(_force, GetComponent<Animator>(), _effects);
        }
    }
}
