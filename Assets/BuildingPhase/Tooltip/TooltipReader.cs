using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace BuilderGame.BuildingPhase.Tooltip
{
    public class TooltipReader : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private GameObject _tooltipContainer;
        [SerializeField] private Vector2 _textOffset;
        [SerializeField] private TextMeshProUGUI _tooltipText;
        [SerializeField] private GraphicRaycaster[] _raycasters;

        private void Update() {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            bool found = false;
            TooltipInteractable tooltip = null;

            tooltip = AnalyzeUI();
            if (tooltip == null)
                tooltip = AnalyzeColliders(mousePosition);

            if (tooltip != null) {
                _tooltipContainer.SetActive(true);
                _tooltipText.text = tooltip.TooltipText;
                _tooltipContainer.transform.position = mousePosition + _textOffset;
            } else {
                _tooltipContainer.SetActive(false);
            }
        }

        private TooltipInteractable AnalyzeColliders(Vector2 mousePosition) {
            TooltipInteractable tooltip = null;
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);
            foreach(Collider2D c in colliders) {
                tooltip = c.gameObject.GetComponent<TooltipInteractable>();
                if (tooltip != null) {
                    break;
                }
            }
            return tooltip;
        }

        private TooltipInteractable AnalyzeUI() {
            TooltipInteractable tooltip = null;

            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = Mouse.current.position.value;
            List<RaycastResult> results = new List<RaycastResult>();
            foreach (GraphicRaycaster raycaster in _raycasters) {
                List<RaycastResult> currentResults = new List<RaycastResult>();
                raycaster.Raycast(pointerEventData, currentResults);
                results.AddRange(currentResults);
            }
            foreach(RaycastResult res in results) {
                tooltip = res.gameObject.GetComponent<TooltipInteractable>();
                if (tooltip != null) {
                    break;
                }
            }
            return tooltip;
        }
    }
}
