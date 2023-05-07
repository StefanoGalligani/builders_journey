using UnityEngine;
using BuilderGame.BuildingPhase.Grid;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.BuildingPhase.SelectionUI {
    public class PieceSelectionManager : MonoBehaviour {
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private PieceSelectable _pieceSelectablePrefab;
        [SerializeField] private PieceInfoScriptableObject[] _pieceInfos;
        [SerializeField] private GridInteractionManager _gridInteractionManager;

        
        private void Start() {
            _scrollContent.sizeDelta = new Vector2(0, 110*_pieceInfos.Length + 10);
            foreach(PieceInfoScriptableObject pieceInfo in _pieceInfos) {
                PieceSelectable pieceSelectable = GameObject.Instantiate<PieceSelectable>(_pieceSelectablePrefab, _scrollContent);
                pieceSelectable.Init(pieceInfo, this);
            }
            StartManagerSingleton.Instance.GameStart += OnGameStart;
        }

        private void OnGameStart() {
            gameObject.SetActive(false);
        }

        public void Selection(PieceSelectable pieceSelectable, Piece prefab) {
            _gridInteractionManager.SetNewPiecePrefab(prefab);
            //gestire feedback visivo della selezione
        }
    }
}