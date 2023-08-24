using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BuilderGame.BuildingPhase;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement
{
    public class SaveUI : SubmenuUI
    {
        [SerializeField] private VehicleSaveManager _saveManager;
        [SerializeField] private TMP_InputField _fileName;

        public void OnSave() {
            _saveManager.SaveOnFile(_fileName.text);
        }

        public void OnLoad() {
            _saveManager.LoadFromFile(_fileName.text);
        }
    }
}
