using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Controls;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(WheelJoint2D))]
    public class WheelPieceController : SpecialPieceController {
        private int _speed;
        private WheelJoint2D _joint;
        private int _motorSpeed = 0;

        internal WheelPieceController(int speed) {
            this._speed = speed;
        }

        internal override void StartPiece() {
            _joint = gameObject.GetComponent<WheelJoint2D>();
        }

        internal override void UpdatePiece()
        {
            AdjustSupensionDir();
        }

        internal override void OnActionExecuted(InputAction.CallbackContext context) {
            _motorSpeed = _speed * (int)context.ReadValue<Single>();
            UpdateMotorSpeed();
        }


        private void UpdateMotorSpeed() {
            if (!_joint) return;
            JointMotor2D m = _joint.motor;
            m.motorSpeed = _motorSpeed;
            _joint.motor = m;
        }

        private void AdjustSupensionDir() {
            if (!_joint || !_joint.connectedBody) return;
            Vector2 connectedBody = _joint.connectedBody.transform.position;
            Vector2 connectedDirection = connectedBody - (Vector2)transform.position;
            float connectedAngle = Vector2.SignedAngle(Vector2.right, connectedDirection);

            JointSuspension2D s = _joint.suspension;
            s.angle = connectedAngle - transform.rotation.eulerAngles.z;
            _joint.suspension = s;
        }
    }
}