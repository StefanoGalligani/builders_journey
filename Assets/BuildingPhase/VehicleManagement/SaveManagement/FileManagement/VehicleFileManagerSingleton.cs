using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    public class VehicleFileManagerSingleton
    {
        private string _filePath;
        private VehicleDataSerializable _vehicleData;
        private bool _fileRead = false;
        private static VehicleFileManagerSingleton _instance;
        public static VehicleFileManagerSingleton Instance {get {return (_instance==null ? (_instance = new VehicleFileManagerSingleton()) : _instance);} private set{} }

        private VehicleFileManagerSingleton()
        {
            _filePath = Application.persistentDataPath + "/Vehicles/";
            if (!Directory.Exists(_filePath))  
            {  
                Directory.CreateDirectory(_filePath);  
            } 
        }

        public bool IsVehicleSaved() {
            if(_fileRead)
                for (int i=0; i<_vehicleData.pieceIds.Length; i++) {
                    Debug.Log("Saved id: " + _vehicleData.pieceIds[i]);
                }
            return _fileRead;
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
            FileStream dataStream = new FileStream(_filePath + fileName, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, _vehicleData);
            dataStream.Close();
            return true;
        }

        internal bool ReadFromFile(string fileName) {
            Debug.Log("File name: " + _filePath + fileName);
            if(!File.Exists(_filePath + fileName)) {
                Debug.LogError("Could not find file " + _filePath + fileName);
                return false;
            }
            FileStream dataStream = new FileStream(_filePath + fileName, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            _vehicleData = converter.Deserialize(dataStream) as VehicleDataSerializable;

            dataStream.Close();
            _fileRead = true;
            return true;
        }
    }
}
