using UnityEngine;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.BuildingPhase.PieceInfo {
    [CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/PieceInfoScriptableObject", order = 1)]
    public class PieceInfoScriptableObject : ScriptableObject
    {
        public int Id;
        public int Price;
        public Piece Prefab;
        public Sprite Sprite;
    }
}