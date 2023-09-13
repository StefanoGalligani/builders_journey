using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using BuilderGame.Utils;
using BuilderGame.Effects;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.Tutorial;
using System.Linq.Expressions;

namespace BuilderGame.BuildingPhase.Builder {
    public class GridInteraction : BuildingPhaseUI, ITutorialElement {
        [SerializeField] private GridInfoScriptableObject _gridInfo;
        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private SubmenuUI _buildingSelectionUI;
        [SerializeField] private SubmenuUI _rebindingUI;
        [SerializeField] private SubmenuUI _savingUI;
        [SerializeField] private SpriteRenderer _selectionSprite;
        [SerializeField] private Image _deletingSelectionImage;
        [SerializeField] private EffectHandler[] _selectionEffects;
        [SerializeField] private EffectHandler[] _placeEffects;
        [SerializeField] private EffectHandler[] _removeEffects;
        private BuilderManager _builderManager;
        private GridState _gridState;
        private GridState _prevState;
        private bool firstEffect = true;

        private void Awake() {
            _buildingSelectionUI.OnToggled += ToggledBuilding;
            _rebindingUI.OnToggled += ToggledRebinding;
            _savingUI.OnToggled += ToggledSaving;
            _deletingSelectionImage.enabled = false;
        }

        private void ChangeState(GridState newState) {
            if (firstEffect) {
                firstEffect = false;
            } else {
                foreach(EffectHandler effect in _selectionEffects) effect.StartEffect();
            }

            if(_gridState != null) _gridState.OnExitState();
            _gridState = newState;
            _gridState.OnEnterState();
        }

        private void ToggledBuilding(bool on) {
            if(on) {
                ChangeState(new GridStateBuilding(_placeEffects));
            }
        }

        private void ToggledRebinding(bool on) {
            if(on) {
                ChangeState(new GridStateRebinding(_selectionSprite));
            }
        }

        private void ToggledSaving(bool on) {
            if(on) {
                ChangeState(new GridStateSaving());
            }
        }

        public void ToggledDeleting() {
            if (_gridState is GridStateDeleting) {
                ChangeState(_prevState);
            } else {
                _prevState = _gridState;
                ChangeState(new GridStateDeleting(_removeEffects, _selectionSprite, _deletingSelectionImage));
            }
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

            if (leftClick) {
                _gridState.OnLeftClick(_builderManager, gridCoords);
            } else {
                _gridState.OnRightClick(_builderManager, gridCoords);
            }
        }

        public void ClickedDeleteAll() {
            _builderManager.RemoveAllPieces();
            foreach(EffectHandler effect in _removeEffects) effect.StartEffect();
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


}