using UnityEngine;

namespace BuilderGame.BuildingPhase.Builder {
    internal abstract class GridState {
        internal abstract void OnEnterState();
        internal abstract void OnExitState();
        internal abstract void OnLeftClick(BuilderManager builder, Vector2Int gridCoords);
        internal abstract void OnRightClick(BuilderManager builder, Vector2Int gridCoords);
    }
}