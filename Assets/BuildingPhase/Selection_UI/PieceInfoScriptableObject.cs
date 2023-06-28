using UnityEngine;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.BuildingPhase.SelectionUI {
    [CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/PieceInfoScriptableObject", order = 1)]
    public class PieceInfoScriptableObject : ScriptableObject
    {
        public int Id;
        public int Price;
        public Piece Prefab;
        public Sprite Sprite;
    }
}