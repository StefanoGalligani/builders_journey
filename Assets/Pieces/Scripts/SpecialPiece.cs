using UnityEngine;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class SpecialPiece : MonoBehaviour {
        protected SpecialPieceController _controller;
        private bool _pieceEnabled;

        private void Start() {
            _pieceEnabled = false;
            StartManagerSingleton.Instance.GameStart += OnGameStart;
            InitController();
            if (_controller != null) _controller.SetGameObject(gameObject);
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
            StartManagerSingleton.Instance.GameStart -= OnGameStart;
        }
    }
}
