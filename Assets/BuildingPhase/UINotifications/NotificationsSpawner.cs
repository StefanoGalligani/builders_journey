using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuilderGame.BuildingPhase.UINotifications
{
    public class NotificationsSpawner : MonoBehaviour
    {
        [SerializeField] private Notification _notificationPrefab;

        public void SpawnNotification(string text) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            position.z = 0;
            Notification notification = Instantiate(_notificationPrefab, position, Quaternion.identity);
            notification.Init(text);
        }
    }
}
