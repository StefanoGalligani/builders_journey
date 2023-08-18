using UnityEngine;
using UnityEngine.InputSystem;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpecialPiece : MonoBehaviour {
        [SerializeField] InputAction action;
        protected SpecialPieceController _controller;
        private bool _pieceEnabled;

        private void Start() {
            _pieceEnabled = false;
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
            InitController();
            if (_controller != null) _controller.SetGameObject(gameObject);
            action.performed += ctx => OnActionExecuted(ctx);
        }

        protected abstract void InitController();

        private void OnGameStart() {
            _pieceEnabled = true;
            if (_controller != null) _controller.StartPiece();
        }
        
        private void Update() {
            if (_pieceEnabled && _controller != null) _controller.UpdatePiece();
        }
        
        private void FixedUpdate() {
            if (_pieceEnabled && _controller != null) _controller.FixedUpdatePiece();
        }

        private void OnActionExecuted(InputAction.CallbackContext context) {
            if (_pieceEnabled && _controller != null) _controller.OnActionExecuted(context);
        }

        private void OnDestroy() {
            if (FindObjectOfType<StartNotifier>())
                FindObjectOfType<StartNotifier>().GameStart -= OnGameStart;
        }

        private void OnEnable()
        {
            action.Enable();
        }

        private void OnDisable()
        {
            action.Disable();
        }
    }
}
