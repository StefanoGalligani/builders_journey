using UnityEngine;
using Cinemachine;
using BuilderGame.BuildingPhase.Start;
using UnityEngine.InputSystem;
using BuilderGame.Input;

namespace BuilderGame.Cam
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        [SerializeField] private Vector3 _originalOffset;
        [SerializeField] private Transform _vehicleTransform;
        private CinemachineFramingTransposer _transposer;
        private Controls _actionAsset;
        private bool _buildingPhase = true;

        private void Start()
        {
            _transposer = _cinemachine.GetComponentInChildren<CinemachineFramingTransposer>();
            _transposer.m_TrackedObjectOffset = _originalOffset;
            _actionAsset = new Controls();
            _actionAsset.Enable();
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void Update() {
            if (_buildingPhase && _actionAsset.defaultmap.CameraMoveActive.IsInProgress()) {
                Vector3 mouseDelta = _actionAsset.defaultmap.CameraMove.ReadValue<Vector2>();
                _transposer.m_TrackedObjectOffset -= mouseDelta * Time.deltaTime * 2;
            }
        }

        private void OnGameStart() {
            _buildingPhase = false;
            _transposer.m_TrackedObjectOffset = _originalOffset;

            _cinemachine.Follow = _vehicleTransform.GetChild(0);
        }
    }
}
