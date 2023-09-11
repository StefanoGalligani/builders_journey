using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BuilderGame.Effects;

namespace BuilderGame.Pieces {
    public class RocketPieceController : SpecialPieceController {
        private float _force;
        private Rigidbody2D _rb;
        private bool _on = false;
        private Animator _animator;
        private List<EffectHandler> _effects;

        internal RocketPieceController(float force, Animator animator, List<EffectHandler> effects) {
            _force = force;
            _animator = animator;
            _effects = effects;
        }

        internal override void StartPiece() {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        internal override void FixedUpdatePiece()
        {
            if (_on) {
                _rb.AddForce(transform.right*_force);
            }
        }

        internal override void OnActionExecuted(InputAction.CallbackContext context) {
            if (context.ReadValue<float>() > 0.5) {
                _on = true;
                foreach(EffectHandler effect in _effects) effect.StartEffect();
            } else {
                _on = false;
                foreach(EffectHandler effect in _effects) effect.StopEffect();
            }
            _animator.SetBool("Active", _on);
        }

        internal override void Interrupt() {
            _on = false;
            foreach(EffectHandler effect in _effects) effect.StopEffect();
            _animator.SetBool("Active", false);
        }
    }
}
