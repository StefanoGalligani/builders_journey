using UnityEngine;
using UnityEngine.UI;
using BuilderGame.Effects;
using BuilderGame.BuildingPhase.Binding;

namespace BuilderGame.BuildingPhase.Builder {
    internal class GridStateDeleting : GridState {
        private EffectHandler[] _removeEffects;
        private SpriteRenderer _selectionSprite;
        private Image _deletingSelectionImage;

        internal GridStateDeleting(EffectHandler[] removeEffects, SpriteRenderer selectionSprite, Image deletingSelectionImage) {
            _removeEffects = removeEffects;
            _selectionSprite = selectionSprite;
            _deletingSelectionImage = deletingSelectionImage;
        }
        internal override void OnEnterState() {
            _deletingSelectionImage.enabled = true;
            GameObject.FindObjectOfType<BindingUI>().EmptyUI();
            _selectionSprite.transform.position = new Vector3(0,-10000, 0);
        }
        internal override void OnExitState() {
            _deletingSelectionImage.enabled = false;
        }
        internal override void OnLeftClick(BuilderManager builder, Vector2Int gridCoords) {
            Delete(builder, gridCoords);
        }

        internal override void OnRightClick(BuilderManager builder, Vector2Int gridCoords) {
            builder.RotatePiece(gridCoords);
        }

        private void Delete(BuilderManager builder, Vector2Int gridCoords) {
            bool removedSuccessfully = builder.PlacePiece(gridCoords, true);
            if (removedSuccessfully) {
                foreach(EffectHandler effect in _removeEffects) effect.StartEffect();
            }
        }
    }
}
