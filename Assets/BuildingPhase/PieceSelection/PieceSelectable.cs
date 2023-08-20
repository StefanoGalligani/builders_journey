using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuilderGame.BuildingPhase.PieceSelection.PieceInfo;

namespace BuilderGame.BuildingPhase.PieceSelection {
    public class PieceSelectable : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _highlight;
        [SerializeField] private TextMeshProUGUI _infoText;
        private PieceInfoScriptableObject _pieceInfo;
        private int _pieceId;
        private PieceSelectionManager _selectionManager;

        internal void Init(PieceInfoScriptableObject pieceInfo, PieceSelectionManager selectionManager) {
            _selectionManager = selectionManager;

            _infoText.text = "$ " + pieceInfo.Price;
            _pieceInfo = pieceInfo;
            _image.sprite = pieceInfo.Sprite;
        }

        public void OnClick() {
            _selectionManager.Selection(this, _pieceInfo);
        }

        internal void ToggleHighlight(bool value) {
            _highlight.enabled = value;
        }
    }
}
