using UnityEngine;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.Builder {
    public class GridInteractionManager : MonoBehaviour
    {
        [SerializeField] private GridInfoScriptableObject _gridInfo;
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private GameObject _content;
        private BuilderManager _builderManager;

        private void Start() {
            _builderManager = new BuilderManager(_gridInfo, _vehicle);
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void OnGameStart() {
            _content.SetActive(false);
        }

        public void SetNewPieceId(int pieceId) {
            _builderManager.NewPieceId = pieceId;
        }

        public void Clicked(bool leftClick) {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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