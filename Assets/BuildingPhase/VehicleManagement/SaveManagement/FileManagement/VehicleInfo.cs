using System.Collections;
using System.Collections.Generic;
using BuilderGame.Utils;
using UnityEngine;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement {
    public struct VehicleInfo : ISelectionInfo
    {
        private string _vehicleName;
        public VehicleInfo(string vehicleName) {
            _vehicleName = vehicleName;
        }

        public string GetVehicleName() {
            return _vehicleName;
        }
    }
}
