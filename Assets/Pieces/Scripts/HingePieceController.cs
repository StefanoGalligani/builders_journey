using UnityEngine;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(HingeJoint2D))]
    public class HingePieceController : SpecialPieceController {
        private int _speed;
        private HingeJoint2D _joint;
        private float _motorSpeed;

        internal HingePieceController(int speed) {
            _speed = speed;
        }

        internal override void StartPiece() {
            _joint = gameObject.GetComponent<HingeJoint2D>();
        }

        internal override void UpdatePiece()
        {
            _motorSpeed = 0;
            if (Input.GetKey(KeyCode.Q)) {
                _motorSpeed -= _speed;
            }
            if (Input.GetKey(KeyCode.E)) {
                _motorSpeed += _speed;
            }
            UpdateMotorSpeed();
        }

        private void UpdateMotorSpeed() {
            if (!_joint) return;
            JointMotor2D m = _joint.motor;
            m.motorSpeed = _motorSpeed;
            _joint.motor = m;
        }
    }
}