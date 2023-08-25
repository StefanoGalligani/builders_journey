using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BuilderGame.Utils;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement
{
    public class SaveUI : SubmenuUI, ISelectionUI<VehicleSelectable, VehicleInfo>
    {

        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private VehicleSelectable _vehicleSelectablePrefab;
        [SerializeField] private VehicleSaveManager _saveManager;
        [SerializeField] private TMP_InputField _fileNameTxt;
        private List<VehicleSelectable> _selectables;
        private string _fileToLoad;

        private void Start() {
            _selectables = new List<VehicleSelectable>();
            string[] fileNames =  VehicleFileAccessSingleton.Instance.GetAllFileNames();
            _scrollContent.sizeDelta = new Vector2(0, 10);
            foreach(string fileName in fileNames) {
                AddVehicleSelectable(fileName);
            }
        }

        public void OnSave() {
            bool saved = _saveManager.SaveOnFile(_fileNameTxt.text);
            if (saved) {
                AddVehicleSelectable(_fileNameTxt.text);
            }
        }

        public void OnLoad() {
            _saveManager.LoadFromFile(_fileToLoad);
        }

        public void Selection(VehicleSelectable vehicleSelectable, VehicleInfo vehicleInfo)
        {
            _fileToLoad = vehicleInfo.GetVehicleName();
            _selectables.ForEach(s => s.ToggleHighlight(s.Equals(vehicleSelectable)));
        }

        public void SelectionDelete(VehicleSelectable vehicleSelectable, VehicleInfo vehicleInfo)
        {
            string fileToDelete = vehicleInfo.GetVehicleName();
            _selectables.Remove(vehicleSelectable);
            Destroy(vehicleSelectable.gameObject);
            _scrollContent.sizeDelta = new Vector2(0, _scrollContent.sizeDelta.y-110);
            VehicleFileAccessSingleton.Instance.DeleteFile(fileToDelete);
        }

        private void AddVehicleSelectable(string name) {
            VehicleSelectable vehicleSelectable = Instantiate<VehicleSelectable>(_vehicleSelectablePrefab, _scrollContent);
            vehicleSelectable.Init(new VehicleInfo(name), this);
            _selectables.Add(vehicleSelectable);
            _scrollContent.sizeDelta = new Vector2(0, _scrollContent.sizeDelta.y+110);
        }
    }
}
