using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.BuildingPhase.SelectionUI {
    public class PieceSelectable : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _highlight;
        [SerializeField] private TextMeshProUGUI _infoText;
        private Piece _piecePrefab;
        private PieceSelectionManager _selectionManager;

        internal void Init(PieceInfoScriptableObject pieceInfo, PieceSelectionManager selectionManager) {
            _selectionManager = selectionManager;

            _infoText.text = "$ " + pieceInfo.Price;
            _piecePrefab = pieceInfo.Prefab;
            _image.sprite = pieceInfo.Sprite;
        }

        public void OnClick() {
            _selectionManager.Selection(this, _piecePrefab);
        }

        internal void ToggleHighlight(bool value) {
            _highlight.enabled = value;
        }
    }
}
