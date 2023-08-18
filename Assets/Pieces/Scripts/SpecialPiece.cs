using UnityEngine;
using UnityEngine.InputSystem;
using BuilderGame.BuildingPhase.Start;
using UnityEngine.InputSystem.Controls;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpecialPiece : MonoBehaviour {
        [SerializeField] InputAction _action;
        protected SpecialPieceController _controller;
        private bool _pieceEnabled;

        private void Start() {
            _pieceEnabled = false;
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
            InitController();
            if (_controller != null) _controller.SetGameObject(gameObject);
            _action.performed += ctx => OnActionExecuted(ctx);
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

        private void OnDestroy() {
            if (FindObjectOfType<StartNotifier>())
                FindObjectOfType<StartNotifier>().GameStart -= OnGameStart;
        }

        private void OnActionExecuted(InputAction.CallbackContext context) {
            if (_pieceEnabled && _controller != null) _controller.OnActionExecuted(context);
        }

        //bindingIndex should be 0 for single bindings, >= 1 for composites
        public void RemapButtonClicked(int bindingIndex)
        {
            _action.Disable();
            var rebindOperation = _action.PerformInteractiveRebinding()
                .WithCancelingThrough("<Keyboard>/escape")
                .WithExpectedControlType(typeof(KeyControl))
                .WithTargetBinding(bindingIndex);
            rebindOperation.Start();
            _action.Enable();
        }

        public string GetRebind() {
            return _action.SaveBindingOverridesAsJson();
        }

        public void LoadRebind(string rebind) {
            _action.LoadBindingOverridesFromJson(rebind);
        }

        private void OnEnable()
        {
            _action.Enable();
        }

        private void OnDisable()
        {
            _action.Disable();
        }
    }
}
