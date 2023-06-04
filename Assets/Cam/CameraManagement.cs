using UnityEngine;
using Cinemachine;
using BuilderGame.BuildingPhase.Builder;

namespace BuilderGame.Cam
{
    public class CameraManagement : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachine;
        private Transform _vehicleTransform;

        void Start()
        {
            _vehicleTransform = GameObject.FindObjectOfType<Vehicle>().transform;
            _cinemachine.Follow = _vehicleTransform;
            StartManagerSingleton.Instance.GameStart += OnStartGame;
        }

        private void OnStartGame() {
            _cinemachine.Follow = _vehicleTransform.GetChild(0);
        }
    }
}
