using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.Grid;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.BuildingPhase.SelectionUI {
    public class PieceSelectionManager : MonoBehaviour {
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private PieceSelectable _pieceSelectablePrefab;
        [SerializeField] private PieceInfoScriptableObject[] _pieceInfos;
        [SerializeField] private GridInteractionManager _gridInteractionManager;
        private List<PieceSelectable> selectables;
        
        private void Start() {
            selectables = new List<PieceSelectable>();
            _scrollContent.sizeDelta = new Vector2(0, 110*_pieceInfos.Length + 10);
            foreach(PieceInfoScriptableObject pieceInfo in _pieceInfos) {
                PieceSelectable pieceSelectable = Instantiate<PieceSelectable>(_pieceSelectablePrefab, _scrollContent);
                pieceSelectable.Init(pieceInfo, this);
                selectables.Add(pieceSelectable);
            }
            StartManagerSingleton.Instance.GameStart += OnGameStart;
        }

        private void OnGameStart() {
            gameObject.SetActive(false);
        }

        public void Selection(PieceSelectable pieceSelectable, Piece prefab) {
            _gridInteractionManager.SetNewPiecePrefab(prefab);
            selectables.ForEach(s => s.ToggleHighlight(s.Equals(pieceSelectable)));
        }
    }
}