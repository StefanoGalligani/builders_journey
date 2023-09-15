using UnityEngine;
using BuilderGame.SpecialPieces;
using BuilderGame.Effects;
using BuilderGame.BuildingPhase.Binding;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.BuildingPhase.Builder {

    internal class GridStateRebinding : GridState {
        private SpriteRenderer _selectionSprite;
        private EffectContainer _selectionEffects;

        internal GridStateRebinding(SpriteRenderer selectionSprite, EffectContainer selectionEffects) {
            _selectionSprite = selectionSprite;
            _selectionEffects = selectionEffects;
        }
        internal override void OnEnterState() {
            _selectionSprite.gameObject.SetActive(true);
        }
        internal override void OnExitState() {
            GameObject.FindObjectOfType<BindingUI>().EmptyUI();
            _selectionSprite.transform.position = new Vector3(0,-10000, 0);
            _selectionSprite.gameObject.SetActive(false);
        }
        internal override void OnLeftClick(BuilderManager builder, Vector2Int gridCoords) {
            Rebind(builder, gridCoords);
            _selectionEffects.StartEffects();
        }
        
        internal override void OnRightClick(BuilderManager builder, Vector2Int gridCoords) {
            builder.RotatePiece(gridCoords);
        }

        private void Rebind(BuilderManager builder, Vector2Int gridCoords) {
            Piece p = builder.GetPieceAtPosition(gridCoords);
            if (p != null && p.GetComponent<SpecialPiece>()) {
                _selectionSprite.gameObject.SetActive(true);
                _selectionSprite.transform.position = p.transform.position;
                GameObject.FindObjectOfType<BindingUI>().PrepareUI(
                    p.GetComponent<SpecialPiece>(),
                    GameObject.FindObjectOfType<PiecesDictionary>().GetSpriteById(p.Id)
                );
            }
        }
    }
}
