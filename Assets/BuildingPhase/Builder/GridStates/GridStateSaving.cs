using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Builder {
    internal class GridStateSaving : GridState {
        internal override void OnEnterState() {}
        internal override void OnExitState() {}
        internal override void OnLeftClick(BuilderManager builder, Vector2Int gridCoords) {
        }

        internal override void OnRightClick(BuilderManager builder, Vector2Int gridCoords) {
            builder.RotatePiece(gridCoords);
        }
    }
}
