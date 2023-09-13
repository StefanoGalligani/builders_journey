using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.BuildingPhase.Builder {
    internal class GridStateBuilding : GridState {
        private EffectHandler[] _placeEffects;

        internal GridStateBuilding(EffectHandler[] placeEffects) {
            _placeEffects = placeEffects;
        }
        internal override void OnEnterState() {}
        internal override void OnExitState() {}

        internal override void OnLeftClick(BuilderManager builder, Vector2Int gridCoords) {
            bool placedSuccessfully = builder.PlacePiece(gridCoords);
            if (placedSuccessfully) {
                foreach(EffectHandler effect in _placeEffects) effect.StartEffect();
            }
        }

        internal override void OnRightClick(BuilderManager builder, Vector2Int gridCoords) {
            builder.RotatePiece(gridCoords);
        }
    }
}
