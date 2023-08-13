using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.Builder;
using BuilderGame.BuildingPhase.PieceInfo;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.BuildingPhase.SelectionUI {
    public class PieceSelectionManager : MonoBehaviour {
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private PieceSelectable _pieceSelectablePrefab;
        [SerializeField] private PieceInfoScriptableObject[] _pieceInfos;
        [SerializeField] private GridInteractionManager _gridInteractionManager;
        [SerializeField] private GameObject _content;
        private List<PieceSelectable> selectables;
        
        private void Awake() {
            FindObjectOfType<PiecesDictionary>().Init(_pieceInfos);
        }

        private void Start() {
            selectables = new List<PieceSelectable>();
            _scrollContent.sizeDelta = new Vector2(0, 110*_pieceInfos.Length + 10);
            foreach(PieceInfoScriptableObject pieceInfo in _pieceInfos) {
                PieceSelectable pieceSelectable = Instantiate<PieceSelectable>(_pieceSelectablePrefab, _scrollContent);
                pieceSelectable.Init(pieceInfo, this);
                selectables.Add(pieceSelectable);
            }
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void OnGameStart() {
            _content.SetActive(false);
        }

        public void Selection(PieceSelectable pieceSelectable, PieceInfoScriptableObject pieceInfo) {
            _gridInteractionManager.SetNewPieceId(pieceInfo.Id);
            selectables.ForEach(s => s.ToggleHighlight(s.Equals(pieceSelectable)));
        }
    }
}