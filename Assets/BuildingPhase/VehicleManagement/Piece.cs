using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using BuilderGame.Utils;

[assembly: InternalsVisibleToAttribute("VehicleManagementTests")]
namespace BuilderGame.BuildingPhase.VehicleManagement {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Piece : MonoBehaviour {
        [SerializeField] internal DirectionEnum[] _availableJointDirections;
        [SerializeField] internal bool _canRotate;
        [SerializeField] internal bool _canBeAttachedTo;
        private Direction _facingDirection;
        private Direction _jointDirection;
        private Rigidbody2D _rb;
        private LineRenderer _lr;
        private AnchoredJoint2D _joint;
        private Rigidbody2D _bodyToConnectTo;
        private bool _isConnected = false;
        private bool _isMainPiece = false;

        public Vector2Int GridPosition{get; private set;}
        public int Id {get; private set;}
        public Direction FacingDirection{get {return _facingDirection;}}
        public bool CanBeAttachedTo {get { return _canBeAttachedTo;}}
        public bool IsConnected {get { return (_isConnected || _isMainPiece);}}

        public void Init(int id, Vector2Int gridPosition, bool isMainPiece = false)
        {
            _rb = GetComponent<Rigidbody2D>();
            _lr = GetComponent<LineRenderer>();
            _joint = GetComponent<AnchoredJoint2D>();

            GridPosition = gridPosition;
            _isMainPiece = isMainPiece;
            Id = id;

            _facingDirection = Direction.Right;
            _jointDirection = Direction.Null;

            if (_lr) {
                _lr.SetPositions(new[] {transform.position, transform.position});
            }
        }

        public void Shift(Direction dir, float distance) {
            Vector2Int shiftCoords = dir;
            GridPosition += shiftCoords;
            transform.position += new Vector3(shiftCoords.x * distance, shiftCoords.y * distance);
            UpdateLineRenderer();
        }

        public void PrepareForGame() {
            if (_rb) _rb.simulated = true;
            if (_lr) _lr.enabled = false;
            ActivateJoint();
        }

        public void Rotate() {
            if(!_canRotate) return;

            for (int i=0; i<4; i++) {
                _facingDirection = _facingDirection + 1;
                transform.Rotate(Vector3.forward * 90);

                bool isValidRotationForJoint = _jointDirection.Equals(Direction.Null) || IsAvailableJointDirection(_jointDirection);
                if (isValidRotationForJoint) break;
            }
        }

        public void SetRotation(Direction newRot) {
            if(!_canRotate) return;

            _facingDirection = newRot;
            transform.Rotate(Vector3.forward * 90 * newRot);
        }

        public void DetachJoint() {
            if (!_joint) return;
            _joint.connectedBody = null;
            _isConnected = false;
            _jointDirection = Direction.Null;
            UpdateLineRenderer();
        }

        public void ConnectJoint(Rigidbody2D other, Direction dir) {
            _bodyToConnectTo = other;
            _isConnected = true;
            _jointDirection = dir;
            if (!IsAvailableJointDirection(_jointDirection)) Rotate();
            UpdateLineRenderer();
        }

        public bool IsPossibleJointDirection(Direction dir) {
            if (_canRotate && _availableJointDirections.Length > 0) return true;
            return IsAvailableJointDirection(dir);
        }

        internal bool IsAvailableJointDirection(Direction dir) {
            return _availableJointDirections.AsEnumerable().Any(dEnum => (dir - _facingDirection).Equals(dEnum));
        }

        private void UpdateLineRenderer() {
            if (!_lr) return;

            if (_jointDirection) {
                _lr.enabled = true;
                _lr.SetPositions(new[] {transform.position - Vector3.forward, transform.position + _jointDirection - Vector3.forward});
            } else {
                _lr.enabled = false;
            }
        }

        private void ActivateJoint() {
            if (!_joint) return;

            _joint.connectedBody = _bodyToConnectTo;
            Vector2 anchorDir = _joint.attachedRigidbody.position - _joint.connectedBody.position;
            _joint.connectedAnchor = anchorDir;
        }
    }
}