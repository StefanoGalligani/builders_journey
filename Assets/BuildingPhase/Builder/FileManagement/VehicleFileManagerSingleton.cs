using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BuilderGame.BuildingPhase.Builder.FileManagement
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
        
        internal VehicleDataSerializable GetVehicleData() {
            if (_vehicleData != null) return _vehicleData;
            Debug.LogWarning("No vehicle data is present");
            return null;
        }

        private void WriteToFile(string fileName) {
            if (!_fileRead) {
                Debug.LogWarning("Tried to write to file without having the data");
                return;
            }
            FileStream dataStream = new FileStream(_filePath + fileName, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, _vehicleData);
            dataStream.Close();
        }

        private void ReadFromFile(string fileName) {
            if(File.Exists(_filePath + fileName)) {
                FileStream dataStream = new FileStream(_filePath + fileName, FileMode.Open);

                BinaryFormatter converter = new BinaryFormatter();
                _vehicleData = converter.Deserialize(dataStream) as VehicleDataSerializable;

                dataStream.Close();
                _fileRead = true;
            } else {
                Debug.LogError("Could not find file " + _filePath + fileName);
            }
        }
    }
}
