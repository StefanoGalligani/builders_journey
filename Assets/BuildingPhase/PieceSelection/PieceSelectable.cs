using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuilderGame.Utils;
using BuilderGame.BuildingPhase.PieceSelection.PieceInfo;
using BuilderGame.BuildingPhase.Tooltip;

namespace BuilderGame.BuildingPhase.PieceSelection {
    public class PieceSelectable : MonoBehaviour, ISelectable
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _highlight;
        [SerializeField] private TextMeshProUGUI _infoText;
        private PieceInfoScriptableObject _pieceInfo;
        private int _pieceId;
        private ISelectionUI<PieceSelectable, PieceInfoScriptableObject> _selectionUI;

        internal void Init(PieceInfoScriptableObject pieceInfo, ISelectionUI<PieceSelectable, PieceInfoScriptableObject> selectionUI) {
            _selectionUI = selectionUI;
            _pieceInfo = pieceInfo;

            bool competitiveMode = PlayerPrefs.GetInt("CompetitiveMode", 0) == 1;

            if (_infoText) {
                _infoText.text = pieceInfo.Price + " $";
                if (!competitiveMode) _infoText.enabled = false;
            }
            if (_image) _image.sprite = pieceInfo.Sprite;
            TooltipInteractable tooltip = GetComponent<TooltipInteractable>();
            if (tooltip != null) tooltip.TooltipText = pieceInfo.PieceName;
        }

        public void OnClick() {
            _selectionUI.Selection(this, _pieceInfo);
        }

        internal void ToggleHighlight(bool value) {
            _highlight.enabled = value;
        }
    }
}
