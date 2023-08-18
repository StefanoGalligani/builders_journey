using UnityEngine;
using UnityEngine.InputSystem;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(WheelJoint2D))]
    public class WheelPieceController : SpecialPieceController {
        private int _speed;
        private WheelJoint2D _joint;
        private int _motorSpeed;

        internal WheelPieceController(int speed) {
            this._speed = speed;
        }

        internal override void StartPiece() {
            _joint = gameObject.GetComponent<WheelJoint2D>();
        }

        internal override void UpdatePiece()
        {
            if (!_joint) return;
            
            _motorSpeed = 0;
            if (Keyboard.current.dKey.isPressed) {
                _motorSpeed += _speed;
            }
            if (Keyboard.current.aKey.isPressed) {
                _motorSpeed -= _speed;
            }

            UpdateMotorSpeed();
            AdjustSupensionDir();
        }

        private void UpdateMotorSpeed() {
            JointMotor2D m = _joint.motor;
            m.motorSpeed = _motorSpeed;
            _joint.motor = m;
        }

        private void AdjustSupensionDir() {
            if (!_joint.connectedBody) return;
            Vector2 connectedBody = _joint.connectedBody.transform.position;
            Vector2 connectedDirection = connectedBody - (Vector2)transform.position;
            float connectedAngle = Vector2.SignedAngle(Vector2.right, connectedDirection);

            JointSuspension2D s = _joint.suspension;
            s.angle = connectedAngle - transform.rotation.eulerAngles.z;
            _joint.suspension = s;
        }
    }
}