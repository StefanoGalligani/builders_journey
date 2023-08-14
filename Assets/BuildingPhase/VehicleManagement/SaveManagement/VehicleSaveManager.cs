using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement
{
    public class VehicleSaveManager : MonoBehaviour
    {
        private Transform _vehicle;
        void Start()
        {
            _vehicle = FindObjectOfType<Vehicle>().transform;
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void SaveVehicle() {
            VehicleDataSerializable vehicleData = new VehicleDataSerializable();
            vehicleData.pieceIds = new int[_vehicle.childCount];
            vehicleData.pieceCoordinates = new int[_vehicle.childCount][];
            vehicleData.pieceRotations = new int[_vehicle.childCount];
            Piece[] pieces = _vehicle.GetComponentsInChildren<Piece>();
            for (int i=0; i<_vehicle.childCount; i++) {
                vehicleData.pieceIds[i] = pieces[i].Id;
                vehicleData.pieceRotations[i] = pieces[i].FacingDirection;
                Vector2Int coord = pieces[i].GridPosition;
                vehicleData.pieceCoordinates[i] = new int[]{coord.x, coord.y};
            }
            VehicleFileManagerSingleton.Instance.SetVehicleData(vehicleData);
        }

        public void SaveOnFile(string name) {
            SaveVehicle();
            VehicleFileManagerSingleton.Instance.WriteToFile(name);
        }

        public void LoadFromFile(string name) {
            bool loaded = VehicleFileManagerSingleton.Instance.ReadFromFile(name);
            if (loaded)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        private void OnGameStart() {
            SaveVehicle();
        }
    }
}
