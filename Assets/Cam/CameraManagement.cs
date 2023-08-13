using UnityEngine;
using Cinemachine;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.Cam
{
    public class CameraManagement : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        [SerializeField] private Vector3 _originalOffset;
        private CinemachineFramingTransposer _transposer;
        private Transform _vehicleTransform;
        private bool _buildingPhase = true;

        void Start()
        {
            _vehicleTransform = GameObject.FindObjectOfType<Vehicle>().transform;
            _transposer = _cinemachine.GetComponentInChildren<CinemachineFramingTransposer>();
            _transposer.m_TrackedObjectOffset = _originalOffset;
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        void Update() {
            if (!_buildingPhase) return;
            if (Input.GetMouseButton(2)) {
                Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
                _transposer.m_TrackedObjectOffset -= mouseDelta * Time.deltaTime * 70;
            }
        }

        private void OnGameStart() {
            _buildingPhase = false;
            _transposer.m_TrackedObjectOffset = _originalOffset;

            _cinemachine.Follow = _vehicleTransform.GetChild(0);
        }
    }
}
