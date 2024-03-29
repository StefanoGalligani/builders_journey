using UnityEngine;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.PieceSelection.PieceInfo {
    [CreateAssetMenu(fileName = "PieceData", menuName = "ScriptableObjects/PieceInfoScriptableObject", order = 1)]
    public class PieceInfoScriptableObject : ScriptableObject, ISelectionInfo
    {
        public int Id;
        public int Price;
        public GameObject Prefab;
        public Sprite Sprite;
        public string PieceName;
    }
}