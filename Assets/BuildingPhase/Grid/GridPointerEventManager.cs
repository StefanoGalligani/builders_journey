using UnityEngine;
using UnityEngine.EventSystems;

namespace BuilderGame.BuildingPhase.Grid {
    public class GridPointerEventManager : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] GridInteractionManager _gridInteractionManager;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) {
                _gridInteractionManager.Clicked(true);
            }
            if (eventData.button == PointerEventData.InputButton.Right) {
                _gridInteractionManager.Clicked(false);
            }
        }
    }
}
