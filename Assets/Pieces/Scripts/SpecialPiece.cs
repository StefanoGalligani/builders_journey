using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using BuilderGame.BuildingPhase.Start;
using BuilderGame.BuildingPhase.Tooltip;
using UnityEngine.InputSystem.Controls;
using Unity.Properties;

[assembly: InternalsVisibleToAttribute("PiecesTests")]
namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpecialPiece : MonoBehaviour {
        public string[] ActionNames;
        [SerializeField] private InputAction _action;
        protected SpecialPieceController _controller;
        private bool _pieceEnabled;
        private int _indexOffset;

        internal void Start() {
            _pieceEnabled = false;
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;

            InitController();
            if (_controller != null) _controller.SetGameObject(gameObject);

            if(_action!=null) _action.performed += ctx => OnActionExecuted(ctx);
            _indexOffset = ActionNames.Length > 1 ? 1 : 0;
            SetTooltipText();
        }

        protected abstract void InitController();

        internal void OnGameStart() {
            _pieceEnabled = true;
            if (_controller != null) _controller.StartPiece();
        }
        
        internal void Update() {
            if (_pieceEnabled && _controller != null) _controller.UpdatePiece();
        }
        
        internal void FixedUpdate() {
            if (_pieceEnabled && _controller != null) _controller.FixedUpdatePiece();
        }

        private void OnDestroy() {
            if (FindObjectOfType<StartNotifier>())
                FindObjectOfType<StartNotifier>().GameStart -= OnGameStart;
        }

        internal void OnActionExecuted(InputAction.CallbackContext context) {
            if (_pieceEnabled && _controller != null) _controller.OnActionExecuted(context);
        }

        public void Interrupt() {
            if (_pieceEnabled && _controller != null) {
                _controller.Interrupt();
                if(_action!=null) _action.Disable();
            }
        }

        //bindingIndex should be 0 for single bindings, >= 1 for composites
        public void RebindButtonClicked(int bindingIndex, Action<string> callback)
        {
            string previousBindingJson = GetBindingJson();
            _action.Disable();
            var rebindOperation = _action.PerformInteractiveRebinding()
                .WithExpectedControlType(typeof(KeyControl))
                .WithMatchingEventsBeingSuppressed(true)
                .WithTargetBinding(bindingIndex + _indexOffset)
                .OnComplete(_ => CheckValidRebind(bindingIndex, previousBindingJson, callback));
            rebindOperation.Start();
            _action.Enable();
        }

        private void CheckValidRebind(int index, string previousBindingJson, Action<string> callback) {
            string canceling = "<Keyboard>/escape";
            
            if (canceling.Equals(_action.bindings[index + _indexOffset].overridePath)) {
                LoadBindingJson(previousBindingJson);
            }
            SetTooltipText();
            callback(GetBindingName(index));
        }

        private void SetTooltipText() {
            TooltipInteractable tooltip = GetComponent<TooltipInteractable>();
            if (tooltip != null) {
                string tooltipText = "";
                for (int i=0; i<ActionNames.Length; i++) {
                    tooltipText += GetBindingName(i);
                    if (i < ActionNames.Length-1) tooltipText += "/";
                }
                tooltip.TooltipText = tooltipText;
            }
        }

        public string GetBindingName(int index) {
            return _action.bindings[index + _indexOffset].effectivePath.Split("/")[1].ToUpper();
        }

        public string GetBindingJson() {
            return _action.SaveBindingOverridesAsJson();
        }

        public void LoadBindingJson(string rebind) {
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
