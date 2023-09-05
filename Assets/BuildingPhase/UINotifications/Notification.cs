using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BuilderGame.BuildingPhase.UINotifications
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _notificationText;
        [SerializeField] private float _speed;
        [SerializeField] private float _ttl;

        internal void Init(string text) {
            _notificationText.text = text;
            StartCoroutine(Movement());
        }

        private IEnumerator Movement() {
            float timePassed = _ttl;
            while (timePassed > 0) {
                transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
                timePassed -= Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
