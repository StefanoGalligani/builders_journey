using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase;
using BuilderGame.BuildingPhase.Builder;
using System.Linq;

namespace BuilderGame {
    public class Vehicle : MonoBehaviour
    {
        private bool _isReadyToStart = false;
        public bool IsReadyToStart {get {return _isReadyToStart;} set {Debug.Log(value?"Ready":"Not ready"); _isReadyToStart=value;}}

        private void Start() {
            StartManagerSingleton.Instance.GameStart += OnGameStart;
        }

        private void OnGameStart() {
            GetComponentsInChildren<Piece>().ToList().ForEach(p => p.PrepareForGame());
        }
    }
}
