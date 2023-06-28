using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.SelectionUI;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.BuildingPhase {
    public class PiecesDictionary : MonoBehaviour {
        private Dictionary<int, Piece> _dictionary;
        internal void Init(PieceInfoScriptableObject[] pieceInfos) {
            _dictionary = new Dictionary<int, Piece>();
            foreach (PieceInfoScriptableObject pieceSo in pieceInfos) {
                _dictionary.Add(pieceSo.Id, pieceSo.Prefab);
            }
        }

        internal Piece GetPrefabById(int id) {
            Piece prefab = null;
            _dictionary.TryGetValue(id, out prefab);
            return prefab;
        }

        internal bool AreAllIdsValid(int[] ids) {
            foreach (int id in ids) {
                if (id>0 && !_dictionary.ContainsKey(id)) return false;
            }
            return true;
        }
    }
}
