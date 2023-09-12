using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Input;
using BuilderGame.Settings;

namespace BuilderGame.Cam {
    public class CamTracker : MonoBehaviour {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        private float _speed;
        private Controls _actionAsset;
        private Rigidbody2D _rb;
        private SettingsFileAccess _settings;

        private void Start() {
            _actionAsset = new Controls();
            _actionAsset.Enable();
            _rb = GetComponent<Rigidbody2D>();
            _settings = FindObjectOfType<SettingsFileAccess>();
            SetSpeed(_settings.GetCameraSensitivity());
            _settings.SettingsUpdated += data => SetSpeed(data.CameraSensitivity);
        }

        private void Update() {
            if (_actionAsset.defaultmap.CameraMoveActive.IsInProgress()) {
                Vector2 mouseDelta = _actionAsset.defaultmap.CameraMove.ReadValue<Vector2>();
                _rb.position -= mouseDelta * Time.deltaTime * _speed;
            }
        }

        private void SetSpeed(float perc) {
            _speed = _minSpeed + (_maxSpeed - _minSpeed) * perc;
        }
    }
}