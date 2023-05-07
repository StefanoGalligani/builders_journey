using UnityEngine;

namespace BuilderGame.BuildingPhase.Builder {
    public class BuilderUIManager : MonoBehaviour
    {
        [SerializeField] private Vehicle _vehicle;

        public void StartGame() {
            if (!_vehicle.IsReadyToStart) return;
            StartManagerSingleton.Instance.StartGame();
        }
    }
}
