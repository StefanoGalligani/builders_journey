using System;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Start {
    public class StartNotifier : MonoBehaviour
    {
        public event Action GameStart;
        [HideInInspector] public bool CanStart;

        public void StartGame() {
            if (!CanStart) return;
            GameStart?.Invoke();
        }
    }
}
