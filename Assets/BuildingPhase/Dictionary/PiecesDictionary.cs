using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using BuilderGame.BuildingPhase.PieceSelection.PieceInfo;

[assembly: InternalsVisibleToAttribute("DictionaryTests")]
namespace BuilderGame.BuildingPhase.Dictionary {
    public class PiecesDictionary : MonoBehaviour {
        [SerializeField] internal GameObject _mainPiecePrefab;
        private Dictionary<int, GameObject> _prefabDictionary;
        private Dictionary<int, int> _priceDictionary;
        private Dictionary<int, Sprite> _spriteDictionary;
        public void Init(PieceInfoScriptableObject[] pieceInfos) {
            _prefabDictionary = new Dictionary<int, GameObject>();
            foreach (PieceInfoScriptableObject pieceSo in pieceInfos) {
                _prefabDictionary.Add(pieceSo.Id, pieceSo.Prefab);
            }
            _prefabDictionary.Add(0, _mainPiecePrefab);

            _priceDictionary = new Dictionary<int, int>();
            foreach (PieceInfoScriptableObject pieceSo in pieceInfos) {
                _priceDictionary.Add(pieceSo.Id, pieceSo.Price);
            }
            _priceDictionary.Add(0, 0);

            _spriteDictionary = new Dictionary<int, Sprite>();
            foreach (PieceInfoScriptableObject pieceSo in pieceInfos) {
                _spriteDictionary.Add(pieceSo.Id, pieceSo.Sprite);
            }
            _spriteDictionary.Add(0, _mainPiecePrefab.GetComponent<SpriteRenderer>().sprite);
        }

        public GameObject GetPrefabById(int id) {
            GameObject prefab = null;
            _prefabDictionary.TryGetValue(id, out prefab);
            return prefab;
        }

        public int GetPriceById(int id) {
            int price = 0;
            _priceDictionary.TryGetValue(id, out price);
            return price;
        }

        public Sprite GetSpriteById(int id) {
            Sprite sprite = null;
            _spriteDictionary.TryGetValue(id, out sprite);
            return sprite;
        }

        public bool AreAllIdsValid(int[] ids) {
            foreach (int id in ids) {
                if (id>0 && !_prefabDictionary.ContainsKey(id)) return false;
            }
            return true;
        }
    }
}
