using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.BuildingPhase.Builder {
    internal class GridStateBuilding : GridState {
        private EffectContainer _placeEffects;

        internal GridStateBuilding(EffectContainer placeEffects) {
            _placeEffects = placeEffects;
        }
        internal override void OnEnterState() {}
        internal override void OnExitState() {}

        internal override void OnLeftClick(BuilderManager builder, Vector2Int gridCoords) {
            bool placedSuccessfully = builder.PlacePiece(gridCoords);
            if (placedSuccessfully) {
                _placeEffects.StartEffects();
            }
        }

        internal override void OnRightClick(BuilderManager builder, Vector2Int gridCoords) {
            builder.RotatePiece(gridCoords);
        }
    }
}
