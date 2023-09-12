using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using BuilderGame.EndingPhase;
using BuilderGame.Settings;

namespace BuilderGame.BuildingPhase.Tooltip
{
    public class TooltipReader : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private GameObject _tooltipContainer;
        [SerializeField] private Vector2 _textOffset;
        [SerializeField] private TextMeshProUGUI _tooltipText;
        [SerializeField] private GraphicRaycaster[] _raycasters;
        private SettingsFileAccess _settings;
        private bool _active;
        private bool _tooltipsOn;

        private void Start() {
            _active = true;
            _tooltipContainer.SetActive(false);
            FindObjectOfType<EndNotifier>().GameEnd += OnGameEnd;

            _settings = FindObjectOfType<SettingsFileAccess>();
            _tooltipsOn = _settings.GetTooltipsOn();
            _settings.SettingsUpdated += data => _tooltipsOn = data.TooltipsOn;
        }

        private void OnGameEnd() {
            _active = false;
            _tooltipContainer.SetActive(false);
        }

        private void Update() {
            if (!_active || !_tooltipsOn) return;

            Vector2 mousePositionScreen = Mouse.current.position.value;
            Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
            TooltipInteractable tooltip = null;

            tooltip = AnalyzeUI(mousePositionScreen);
            if (tooltip == null)
                tooltip = AnalyzeColliders(mousePositionWorld);

            if (tooltip != null) {
                _tooltipContainer.SetActive(true);
                _tooltipText.text = tooltip.TooltipText;
                _tooltipContainer.transform.position = mousePositionWorld + _textOffset;

                Vector2 mousePosPercentage = new Vector2(
                                                mousePositionScreen.x / Screen.currentResolution.width, 
                                                mousePositionScreen.y / Screen.currentResolution.height);
                if (mousePosPercentage.x > 0.6)
                    _tooltipContainer.transform.position += new Vector3(-2*_textOffset.x, 0, 0);
                if (mousePosPercentage.y > 0.6)
                    _tooltipContainer.transform.position += new Vector3(0, -2*_textOffset.y, 0);
            } else {
                _tooltipContainer.SetActive(false);
            }
        }
        private TooltipInteractable AnalyzeUI(Vector2 mousePositionScreen) {
            TooltipInteractable tooltip = null;

            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = mousePositionScreen;
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

        private TooltipInteractable AnalyzeColliders(Vector2 mousePositionWorld) {
            TooltipInteractable tooltip = null;
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePositionWorld);
            foreach(Collider2D c in colliders) {
                tooltip = c.gameObject.GetComponent<TooltipInteractable>();
                if (tooltip != null) {
                    break;
                }
            }
            return tooltip;
        }

    }
}
