using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using BuilderGame.Utils;
using BuilderGame.Effects;
using BuilderGame.Pieces;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.Binding;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.Tutorial;

namespace BuilderGame.BuildingPhase.Builder {
    public class GridInteraction : BuildingPhaseUI, ITutorialElement
    {
        [SerializeField] private GridInfoScriptableObject _gridInfo;
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private SubmenuUI _buildingSelectionUI;
        [SerializeField] private SubmenuUI _rebindingUI;
        [SerializeField] private SubmenuUI _savingUI;
        [SerializeField] private SpriteRenderer _selectionSprite;
        [SerializeField] private Image _deletingSelectionImage;
        [SerializeField] private EffectHandler[] _effects;
        private BuilderManager _builderManager;
        private GridState _gridState;
        private GridState _prevState;

        private void Awake() {
            _buildingSelectionUI.OnToggled += ToggledBuilding;
            _rebindingUI.OnToggled += ToggledRebinding;
            _savingUI.OnToggled += ToggledSaving;
            _deletingSelectionImage.enabled = false;
        }

        private void ToggledBuilding(bool on) {
            if(on) _gridState = GridState.Building;
            ToggledDeleting(false);
        }

        private void ToggledRebinding(bool on) {
            if(on) _gridState = GridState.Rebinding;
            else {
                GameObject.FindObjectOfType<BindingUI>().EmptyUI();
                _selectionSprite.transform.position = new Vector3(0,-10000, 0);
            }
            _selectionSprite.gameObject.SetActive(on);
        }

        private void ToggledSaving(bool on) {
            if(on) _gridState = GridState.Saving;
        }

        public void ToggledDeleting(bool directClick) {
            if (_gridState == GridState.Deleting || !directClick) {
                if(directClick) _gridState = _prevState;
                _deletingSelectionImage.enabled = false;
            } else {
                _prevState = _gridState;
                _gridState = GridState.Deleting;
                _deletingSelectionImage.enabled = true;
                GameObject.FindObjectOfType<BindingUI>().EmptyUI();
                _selectionSprite.transform.position = new Vector3(0,-10000, 0);
            }
            if (directClick)
                foreach(EffectHandler effect in _effects) effect.StartEffect();
        }

        protected override void Init() {
            _builderManager = new BuilderManager(_gridInfo, _vehicle);
        }

        public void SetNewPieceId(int pieceId) {
            ToggledBuilding(true);
            _builderManager.NewPieceId = pieceId;
        }

        public void Clicked(bool leftClick) {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2Int gridCoords = PositionToGridCoordinates(clickPosition);
            if (gridCoords.x >= _gridInfo.GridDimensions.x || gridCoords.y >= _gridInfo.GridDimensions.y) return;

            switch (_gridState) {
                case GridState.Building:
                    if (leftClick) {
                        _builderManager.PlacePiece(gridCoords);
                    } else {
                        _builderManager.RotatePiece(gridCoords);
                    }
                    break;
                case GridState.Rebinding:
                    Piece p = _builderManager.GetPieceAtPosition(gridCoords);
                    if (p != null && p.GetComponent<SpecialPiece>()) {
                        _selectionSprite.gameObject.SetActive(true);
                        _selectionSprite.transform.position = p.transform.position;
                        GameObject.FindObjectOfType<BindingUI>()
                        .PrepareUI(
                            p.GetComponent<SpecialPiece>(),
                            FindObjectOfType<PiecesDictionary>().GetSpriteById(p.Id)
                        );
                    }
                    break;
                case GridState.Saving:
                    if (!leftClick) {
                        _builderManager.RotatePiece(gridCoords);
                    }
                    break;
                case GridState.Deleting:
                    _builderManager.PlacePiece(gridCoords, true);
                    break;
            }

            /*if (_gridState == GridState.Building) {
                if (leftClick) {
                    _builderManager.PlacePiece(gridCoords);
                } else {
                    _builderManager.RotatePiece(gridCoords);
                }
            } else if (_gridState == GridState.Rebinding) {
                Piece p = _builderManager.GetPieceAtPosition(gridCoords);
                if (p != null && p.GetComponent<SpecialPiece>()) {
                    _selectionSprite.gameObject.SetActive(true);
                    _selectionSprite.transform.position = p.transform.position;
                    GameObject.FindObjectOfType<BindingUI>()
                    .PrepareUI(
                        p.GetComponent<SpecialPiece>(),
                        FindObjectOfType<PiecesDictionary>().GetSpriteById(p.Id)
                    );
                }
            } else if (_gridState == GridState.Deleting) {
                _builderManager.PlacePiece(gridCoords, true);
            }*/
        }

        public void ClickedDeleteAll() {
            _builderManager.RemoveAllPieces();
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

        void ITutorialElement.DisableInTutorial()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        void ITutorialElement.EnableInTutorial()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    internal enum GridState {
        Building,
        Rebinding,
        Saving,
        Deleting
    }
}