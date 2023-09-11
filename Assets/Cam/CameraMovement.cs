using UnityEngine;
using Cinemachine;
using BuilderGame.BuildingPhase.Start;
using UnityEngine.InputSystem;
using BuilderGame.Input;

namespace BuilderGame.Cam {
    public class CameraMovement : MonoBehaviour {
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        [SerializeField] private CamTracker _camTracker;
        [SerializeField] private Transform _vehicleTransform;
        [SerializeField] private Vector3 _defaultOffset;

        private void Start() {
            _camTracker.transform.position = _vehicleTransform.position + _defaultOffset;
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }


        private void OnGameStart() {
            _cinemachine.Follow = _vehicleTransform.GetChild(0);
            _camTracker.enabled = false;
        }
    }
}
