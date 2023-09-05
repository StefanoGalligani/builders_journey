using System;
using UnityEngine;
using BuilderGame.BuildingPhase.Tutorial;
using BuilderGame.BuildingPhase.UINotifications;

namespace BuilderGame.BuildingPhase.Start {
    public class StartNotifier : MonoBehaviour, ITutorialElement
    {
        public event Action GameStart;
        [HideInInspector] public bool CanStart;
        private NotificationsSpawner _notificationsSpawner;

        private void Start() {
            _notificationsSpawner = FindObjectOfType<NotificationsSpawner>();
        }

        public void StartGame() {
            if (!CanStart) {
                _notificationsSpawner?.SpawnNotification("The vehicle is not fully connected");
                return;
            }
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
