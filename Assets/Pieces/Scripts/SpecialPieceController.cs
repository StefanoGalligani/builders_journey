using UnityEngine;
using UnityEngine.InputSystem;

namespace BuilderGame.SpecialPieces {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpecialPieceController {

        protected GameObject gameObject;
        protected Transform transform;

        internal void SetGameObject(GameObject go) {
            gameObject = go;
            transform = go.transform;
        }

        internal virtual void StartPiece(){}

        internal virtual void UpdatePiece(){}

        internal virtual void FixedUpdatePiece(){}

        internal abstract void OnActionExecuted(InputAction.CallbackContext context);

        internal abstract void Interrupt();
    }
}
