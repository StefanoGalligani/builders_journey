using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BuilderGame.Effects;

namespace BuilderGame.BuildingPhase.UINotifications {
    public class NotificationsSpawner : MonoBehaviour {
        [SerializeField] private Notification _notificationPrefab;
        [SerializeField] private EffectContainer _effects;

        public void SpawnNotification(string text) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            position.z = 0;
            Notification notification = Instantiate(_notificationPrefab, position, Quaternion.identity);
            notification.Init(text);
            _effects.StartEffects();
        }
    }
}
