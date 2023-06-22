using UnityEngine;

namespace BuilderGame.Pieces {
    [RequireComponent(typeof(HingeJoint2D))]
    public class HingePieceController : SpecialPieceController {
        private int _speed;
        private SpriteRenderer _baseSprite;
        private HingeJoint2D _joint;
        private float _motorSpeed;

        internal HingePieceController(int speed, SpriteRenderer baseSprite) {
            _speed = speed;
            _baseSprite = baseSprite;
        }

        internal override void StartPiece() {
            _joint = gameObject.GetComponent<HingeJoint2D>();
            float angle = _baseSprite.transform.eulerAngles.z * Mathf.PI / 180;
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
            AdjustBaseSpriteRotation();
        }

        private void UpdateMotorSpeed() {
            if (!_joint) return;
            JointMotor2D m = _joint.motor;
            m.motorSpeed = _motorSpeed;
            _joint.motor = m;
        }

        private void AdjustBaseSpriteRotation() {
            if (!_joint.connectedBody) return;
            Vector2 connectedBody = _joint.connectedBody.transform.position;
            Vector2 directionFromConnected = (Vector2)transform.position - connectedBody;
            float connectedAngle = Vector2.SignedAngle(Vector2.up, directionFromConnected);
            _baseSprite.transform.rotation = Quaternion.Euler(0, 0, connectedAngle);
        }
    }
}