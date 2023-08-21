using UnityEngine;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.PieceSelection.PieceInfo {
    [CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/PieceInfoScriptableObject", order = 1)]
    public class PieceInfoScriptableObject : ScriptableObject, IInfoScriptableObject
    {
        public int Id;
        public int Price;
        public Piece Prefab;
        public Sprite Sprite;
    }
}