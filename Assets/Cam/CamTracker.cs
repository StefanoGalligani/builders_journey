using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Input;

namespace BuilderGame.Cam {
    public class CamTracker : MonoBehaviour {
        private Controls _actionAsset;
        private Rigidbody2D _rb;
        void Start() {
            _actionAsset = new Controls();
            _actionAsset.Enable();
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update() {
            if (_actionAsset.defaultmap.CameraMoveActive.IsInProgress()) {
                Vector2 mouseDelta = _actionAsset.defaultmap.CameraMove.ReadValue<Vector2>();
                _rb.position -= mouseDelta * Time.deltaTime * 2;
            }
        }
    }
}