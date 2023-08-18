using UnityEngine;
using BuilderGame.BuildingPhase.Start;
using System.Linq;

namespace BuilderGame.BuildingPhase.VehicleManagement {
    public class Vehicle : MonoBehaviour
    {
        private void Start() {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void OnGameStart() {
            GetComponentsInChildren<Piece>().ToList().ForEach(p => p.PrepareForGame());
        }
    }
}
