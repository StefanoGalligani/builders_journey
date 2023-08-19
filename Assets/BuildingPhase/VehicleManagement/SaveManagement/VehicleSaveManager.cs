using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;
using BuilderGame.Pieces;
using BuilderGame.BuildingPhase.Start;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement
{
    public class VehicleSaveManager : MonoBehaviour
    {
        [SerializeField] private Transform _vehicleTransform;
        void Start()
        {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void SaveVehicle() {
            VehicleDataSerializable vehicleData = new VehicleDataSerializable();
            vehicleData.pieceIds = new int[_vehicleTransform.childCount];
            vehicleData.pieceRotations = new int[_vehicleTransform.childCount];
            vehicleData.pieceCoordinates = new int[_vehicleTransform.childCount][];
            vehicleData.bindings = new string[_vehicleTransform.childCount];
            Piece[] pieces = _vehicleTransform.GetComponentsInChildren<Piece>();
            for (int i=0; i<_vehicleTransform.childCount; i++) {
                vehicleData.pieceIds[i] = pieces[i].Id;
                vehicleData.pieceRotations[i] = pieces[i].FacingDirection;
                Vector2Int coord = pieces[i].GridPosition;
                vehicleData.pieceCoordinates[i] = new int[]{coord.x, coord.y};
                vehicleData.bindings[i] = "";
                SpecialPiece sp = pieces[i].gameObject.GetComponent<SpecialPiece>();
                if (sp) {
                    vehicleData.bindings[i] = sp.GetBindingJson();
                }
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
