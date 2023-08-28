using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    public class VehicleFileAccessSingleton
    {
        private string _filePath;
        private VehicleDataSerializable _vehicleData;
        private bool _fileRead = false;
        private List<string> _fileNames;
        private static VehicleFileAccessSingleton _instance;
        public static VehicleFileAccessSingleton Instance {get {return (_instance==null ? (_instance = new VehicleFileAccessSingleton()) : _instance);} private set{} }

        private VehicleFileAccessSingleton()
        {
            _filePath = Application.persistentDataPath + "/Vehicles/";
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
            FileStream dataStream = new FileStream(_filePath + fileName, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, _vehicleData);
            dataStream.Close();
            return true;
        }

        internal bool ReadFromFile(string fileName) {
            if(!File.Exists(_filePath + fileName)) {
                Debug.LogError("Could not find file " + _filePath + fileName);
                return false;
            }
            FileStream dataStream = new FileStream(_filePath + fileName, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            try {
                _vehicleData = converter.Deserialize(dataStream) as VehicleDataSerializable;
            } catch (SerializationException e) {
                Debug.LogError("File was not valid.\n" + e.StackTrace);
                return false;
            }

            dataStream.Close();
            _fileRead = true;
            return true;
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
