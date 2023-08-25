using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BuilderGame.Utils;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement {
    public class VehicleSelectable : MonoBehaviour, ISelectable
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _highlight;
        private VehicleInfo _vehicleInfo;
        private ISelectionUI<VehicleSelectable, VehicleInfo> _selectionUI;

        internal void Init(VehicleInfo vehicleInfo, ISelectionUI<VehicleSelectable, VehicleInfo> selectionUI) {
            _selectionUI = selectionUI;
            _vehicleInfo = vehicleInfo;
            _nameText.text = vehicleInfo.GetVehicleName();
        }

        public void OnClick() {
            _selectionUI.Selection(this, _vehicleInfo);
        }

        public void OnClickDelete() {
            ((SaveUI)_selectionUI).SelectionDelete(this, _vehicleInfo);
        }

        internal void ToggleHighlight(bool value) {
            _highlight.enabled = value;
        }
    }
}
