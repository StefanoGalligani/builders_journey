using UnityEngine;
using BuilderGame.BuildingPhase.Builder;
using System.Linq;

namespace BuilderGame.BuildingPhase {
    public class Vehicle : MonoBehaviour
    {
        private bool _isReadyToStart = false;
        public bool IsReadyToStart {get {return _isReadyToStart;} set {Debug.Log(value?"Ready":"Not ready"); _isReadyToStart=value;}}

        private void Start() {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void OnGameStart() {
            GetComponentsInChildren<Piece>().ToList().ForEach(p => p.PrepareForGame());
        }
    }
}
