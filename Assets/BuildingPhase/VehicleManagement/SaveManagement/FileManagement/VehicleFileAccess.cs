using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    public class VehicleFileAccess : MonoBehaviour {
        [SerializeField ] private string _directory = "Vehicles";
        private string _filePath;
        private VehicleDataSerializable _vehicleData;
        private bool _fileRead = false;
        private List<string> _fileNames;

        private void Awake() {
            _filePath = Application.persistentDataPath + "/" + _directory + "/";
            if (!Directory.Exists(_filePath))  
            {  
                Directory.CreateDirectory(_filePath);  
            } 
        }

        public bool IsVehicleSaved() {
            return _fileRead;
        }

        public List<string> GetAllFileNames() {
            if (_fileNames == null) {
                _fileNames = Directory.GetFiles(_filePath)
                .Select(file => Path.GetFileName(file)).ToList();
            }
            return _fileNames;
        }

        internal void SetVehicleData(VehicleDataSerializable data) {
            _vehicleData = data;
            _fileRead = true;
        }
        
        public VehicleDataSerializable GetVehicleData() {
            if (_vehicleData != null) return _vehicleData;
            Debug.LogWarning("No vehicle data is present");
            return null;
        }

        internal bool WriteToFile(string fileName) {
            if (!_fileRead) {
                Debug.LogWarning("Tried to write to file without having the data");
                return false;
            }
            if (!_fileNames.Contains(fileName)) _fileNames.Add(fileName);
            bool success;
            FileHelper.Write(_vehicleData, _filePath + fileName, out success);
            return success;
        }

        internal bool ReadFromFile(string fileName) {
            if(!File.Exists(_filePath + fileName)) {
                Debug.LogError("Could not find file " + _filePath + fileName);
                return false;
            }
            bool success;
            _vehicleData = FileHelper.Read<VehicleDataSerializable>(_filePath + fileName, out success);
            _fileRead = true;
            return success;
        }

        internal void DeleteFile(string fileName) {
            if(!File.Exists(_filePath + fileName)) {
                Debug.LogError("Could not find file " + _filePath + fileName);
                return;
            }
            if (_fileNames.Contains(fileName)) _fileNames.Remove(fileName);
            File.Delete(_filePath + fileName);
        }
    }
}
