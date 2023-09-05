using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuilderGame.Pieces;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase;
using TMPro;

namespace BuilderGame.BuildingPhase.Binding
{
    public class BindingUI : SubmenuUI
    {
        [SerializeField] private BindingInfo _bindingInfoPrefab;
        [SerializeField] private GameObject _infoContainer;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _hintText;
        private BindingInfo[] _bindingInfos;
        private SpecialPiece _piece;

        private void Start() {
            _bindingInfos = new BindingInfo[0];
            _hintText.gameObject.SetActive(true);
            _image.gameObject.SetActive(false);
        }

        public void PrepareUI(SpecialPiece piece, Sprite sprite) {
            foreach(BindingInfo g in _bindingInfos) {
                g.OnRebind -= RebindAction;
                Destroy(g.gameObject);
            }
            _piece = piece;
            _bindingInfos = new BindingInfo[piece.ActionNames.Length];
            for (int i=0; i<_bindingInfos.Length; i++) {
                _bindingInfos[i] = Instantiate<BindingInfo>(_bindingInfoPrefab, _infoContainer.transform);
                _bindingInfos[i].Init(piece.ActionNames[i], piece.GetBindingName(i), i);
                _bindingInfos[i].OnRebind += RebindAction;
            }
            _hintText.gameObject.SetActive(false);
            _image.gameObject.SetActive(true);
            _image.sprite = sprite;
        }

        public void EmptyUI() {
            if (_bindingInfos != null) {
                foreach(BindingInfo g in _bindingInfos) {
                    g.OnRebind -= RebindAction;
                    Destroy(g.gameObject);
                }
            }
            _bindingInfos = new BindingInfo[0];
            _hintText.gameObject.SetActive(true);
            _image.gameObject.SetActive(false);
        }

        private void RebindAction(int actionNumber) {
            _piece.RebindButtonClicked(actionNumber, _bindingInfos[actionNumber].UpdateBindingName);
        }
    }
}
