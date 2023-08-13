using UnityEngine;

namespace BuilderGame.BuildingPhase.Builder {
    [CreateAssetMenu(fileName = "GridData", menuName = "ScriptableObjects/GridInfoScriptableObject", order = 1)]
    public class GridInfoScriptableObject : ScriptableObject
    {
        public Vector2Int GridDimensions;
        public Vector2Int MainPieceCoordinates;
        public Vector2 BottomLeftCoords;
        public Vector2 GridOffset;
        public float CellSize;
    }
}
