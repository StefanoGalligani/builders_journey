using UnityEngine;
using BuilderGame.Utils;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.VehicleManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

namespace BuilderGame.BuildingPhase.Builder {
    public class GridInteraction : BuildingPhaseUI
    {
        [SerializeField] private GridInfoScriptableObject _gridInfo;
        [SerializeField] private Vehicle _vehicle;
        private BuilderManager _builderManager;

        protected override void Init() {
            _builderManager = new BuilderManager(_gridInfo, _vehicle);
        }

        public void SetNewPieceId(int pieceId) {
            _builderManager.NewPieceId = pieceId;
        }

        public void Clicked(bool leftClick) {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2Int gridCoords = PositionToGridCoordinates(clickPosition);
            if (gridCoords.x >= _gridInfo.GridDimensions.x || gridCoords.y >= _gridInfo.GridDimensions.y) return;
            if (leftClick) {
                _builderManager.PlacePiece(gridCoords);
            } else {
                _builderManager.RotatePiece(gridCoords);
            }
        }

        public void ClickedShift(int direction) {
            _builderManager.ShiftAllPieces(direction);
        }

        private Vector2Int PositionToGridCoordinates(Vector2 pos) {
            pos -= _gridInfo.BottomLeftCoords;
            Vector2Int coords = new Vector2Int((int)pos.x, (int)pos.y);
            return coords;
        }
    }
}