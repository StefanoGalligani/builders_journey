using System;
using UnityEngine;
using BuilderGame.BuildingPhase.Tutorial;

namespace BuilderGame.BuildingPhase.Start {
    public class StartNotifier : MonoBehaviour, ITutorialElement
    {
        public event Action GameStart;
        [HideInInspector] public bool CanStart;

        public void StartGame() {
            if (!CanStart) return;
            GameStart?.Invoke();
            gameObject.SetActive(false);
        }

        void ITutorialElement.DisableInTutorial()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        void ITutorialElement.EnableInTutorial()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
