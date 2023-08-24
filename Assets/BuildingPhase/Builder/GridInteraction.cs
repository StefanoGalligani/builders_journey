using UnityEngine;
using BuilderGame.Utils;
using BuilderGame.Pieces;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.Binding;
using BuilderGame.BuildingPhase.VehicleManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

namespace BuilderGame.BuildingPhase.Builder {
    public class GridInteraction : BuildingPhaseUI
    {
        [SerializeField] private GridInfoScriptableObject _gridInfo;
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private SubmenuUI _buildingSelectionUI;
        [SerializeField] private SubmenuUI _rebindingUI;
        [SerializeField] private SpriteRenderer _selectionSprite;
        private BuilderManager _builderManager;
        private bool _building;
        private bool _rebinding;

        private void Awake() {
            _buildingSelectionUI.OnToggled += ToggledBuilding;
            _rebindingUI.OnToggled += ToggledRebinding;
        }

        private void ToggledBuilding(bool on) { _building = on;}

        private void ToggledRebinding(bool on) { _rebinding = on; _selectionSprite.gameObject.SetActive(on);}

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

            if (_building) {
                if (leftClick) {
                    _builderManager.PlacePiece(gridCoords);
                } else {
                    _builderManager.RotatePiece(gridCoords);
                }
            } else if (_rebinding) {
                Piece p = _builderManager.GetPieceAtPosition(gridCoords);
                if (p != null && p.GetComponent<SpecialPiece>()) {
                    _selectionSprite.transform.position = p.transform.position;
                    GameObject.FindObjectOfType<BindingUI>()
                    .PrepareUI(
                        p.GetComponent<SpecialPiece>(),
                        FindObjectOfType<PiecesDictionary>().GetSpriteById(p.Id)
                    );
                }
            }
        }

        public void ClickedShift(int direction) {
            _builderManager.ShiftAllPieces(direction);
        }

        protected override void DoOnGameStart() {
            _selectionSprite.enabled = false;
        }

        private Vector2Int PositionToGridCoordinates(Vector2 pos) {
            pos -= _gridInfo.BottomLeftCoords;
            Vector2Int coords = new Vector2Int((int)pos.x, (int)pos.y);
            return coords;
        }
    }
}