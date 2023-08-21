using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.Builder;
using BuilderGame.BuildingPhase.PieceSelection.PieceInfo;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.Utils;

[assembly: InternalsVisibleToAttribute("PieceSelectionTests")]
namespace BuilderGame.BuildingPhase.PieceSelection {
    public class PieceSelectionUI : BuildingPhaseUI, ISelectionUI<PieceSelectable, PieceInfoScriptableObject> {
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private PieceSelectable _pieceSelectablePrefab;
        [SerializeField] private PieceInfoScriptableObject[] _pieceInfos;
        [SerializeField] private GridInteraction _gridInteractionManager;
        private List<PieceSelectable> selectables;
        
        private void Awake() {
            FindObjectOfType<PiecesDictionary>().Init(_pieceInfos);
        }

        protected override void Init() {
            selectables = new List<PieceSelectable>();
            _scrollContent.sizeDelta = new Vector2(0, 110*_pieceInfos.Length + 10);
            foreach(PieceInfoScriptableObject pieceInfo in _pieceInfos) {
                PieceSelectable pieceSelectable = Instantiate<PieceSelectable>(_pieceSelectablePrefab, _scrollContent);
                pieceSelectable.Init(pieceInfo, this);
                selectables.Add(pieceSelectable);
            }
        }

        public void Selection(PieceSelectable pieceSelectable, PieceInfoScriptableObject pieceInfo) {
            _gridInteractionManager.SetNewPieceId(pieceInfo.Id);
            selectables.ForEach(s => s.ToggleHighlight(s.Equals(pieceSelectable)));
        }
    }
}