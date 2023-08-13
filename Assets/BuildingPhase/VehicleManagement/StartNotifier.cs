using System;
using UnityEngine;

namespace BuilderGame.BuildingPhase.VehicleManagement {
    public class StartNotifier : MonoBehaviour
    {
        public event Action GameStart;
        [SerializeField] private Vehicle _vehicle;

        public void StartGame() {
            if (!_vehicle.IsReadyToStart) return;
            GameStart?.Invoke();
        }
    }
}
