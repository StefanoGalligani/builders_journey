using UnityEngine;
using UnityEngine.SceneManagement;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.UINotifications;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;
using BuilderGame.Pieces;
using BuilderGame.BuildingPhase.Start;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement
{
    public class VehicleSaveManager : MonoBehaviour
    {
        [SerializeField] private Transform _vehicleTransform;
        private PiecesDictionary _piecesDictionary;
        private NotificationsSpawner _notificationsSpawner;
        void Start()
        {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
            _piecesDictionary = FindObjectOfType<PiecesDictionary>();
            _notificationsSpawner = FindObjectOfType<NotificationsSpawner>();
        }

        private void SaveVehicle() {
            VehicleDataSerializable vehicleData = new VehicleDataSerializable();
            vehicleData.data = new PieceDataSerializable[_vehicleTransform.childCount];
            Piece[] pieces = _vehicleTransform.GetComponentsInChildren<Piece>();
            for (int i=0; i<_vehicleTransform.childCount; i++) {
                vehicleData.data[i].pieceId = pieces[i].Id;
                vehicleData.data[i].pieceRotation = pieces[i].FacingDirection;
                Vector2Int coord = pieces[i].GridPosition;
                vehicleData.data[i].pieceCoordinates = new int[]{coord.x, coord.y};
                vehicleData.data[i].binding = "";
                SpecialPiece sp = pieces[i].gameObject.GetComponent<SpecialPiece>();
                if (sp) {
                    vehicleData.data[i].binding = sp.GetBindingJson();
                }
            }
            VehicleFileAccessSingleton.Instance.SetVehicleData(vehicleData);
        }

        public bool SaveOnFile(string name) {
            SaveVehicle();
            return VehicleFileAccessSingleton.Instance.WriteToFile(name);
        }

        public void LoadFromFile(string name) {
            bool loaded = VehicleFileAccessSingleton.Instance.ReadFromFile(name);
            if (loaded) {
                if (!_piecesDictionary.AreAllIdsValid(VehicleFileAccessSingleton.Instance.GetVehicleData().GetAllIds())) {
                    _notificationsSpawner.SpawnNotification("The loaded vehicle contains pieces that are not available in this level");
                    return;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }


        private void OnGameStart() {
            SaveVehicle();
        }
    }
}
