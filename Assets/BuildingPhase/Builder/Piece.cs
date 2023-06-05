using UnityEngine;
using BuilderGame.Utils;

namespace BuilderGame.BuildingPhase.Builder {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Piece : MonoBehaviour {
        [SerializeField] private DirectionEnum[] _availableJointDirections;
        [SerializeField] private bool _canRotate;
        [SerializeField] private bool _canBeAttachedTo;
        private Direction _facingDirection;
        private Direction _jointDirection;
        private Rigidbody2D _rb;
        private LineRenderer _lr;
        private AnchoredJoint2D _joint;
        private Rigidbody2D _bodyToConnectTo;
        private bool _isConnected = false;
        private bool _isMainPiece = false;

        internal Vector2Int GridPosition{get; private set;}
        internal bool CanBeAttachedTo {get { return _canBeAttachedTo;}}
        internal bool IsConnected {get { return (_isConnected || _isMainPiece);}}

        internal void Init(Vector2Int gridPosition, bool isMainPiece = false)
        {
            _rb = GetComponent<Rigidbody2D>();
            _lr = GetComponent<LineRenderer>();
            _joint = GetComponent<AnchoredJoint2D>();

            GridPosition = gridPosition;
            _isMainPiece = isMainPiece;

            _facingDirection = Direction.Right;
            _jointDirection = Direction.Null;

            if (_lr) {
                _lr.SetPositions(new[] {transform.position, transform.position});
            }
        }

        public void PrepareForGame() {
            _rb.simulated = true;
            if (_lr) _lr.enabled = false;
            ActivateJoint();
        }


        internal void Rotate() {
            if(!_canRotate) return;

            for (int i=0; i<4; i++) {
                _facingDirection = _facingDirection + 1;
                transform.Rotate(Vector3.forward * 90);

                if (_jointDirection.Equals(Direction.Null)) break;
                if (IsAvailableJointDirection(_jointDirection)) break;
            }
        }

        internal void DetachJoint() {
            if (!_joint) return;
            _joint.connectedBody = null;
            _isConnected = false;
            _jointDirection = Direction.Null;
            UpdateLineRenderer();
        }

        internal void ConnectJoint(Rigidbody2D other, Direction dir) {
            _bodyToConnectTo = other;
            _isConnected = true;
            _jointDirection = dir;
            if (!IsAvailableJointDirection(_jointDirection)) Rotate();
            UpdateLineRenderer();
        }

        internal bool IsPossibleJointDirection(Direction dir) {
            if (_canRotate && _availableJointDirections.Length > 0) return true;
            return IsAvailableJointDirection(dir);
        }

        private bool IsAvailableJointDirection(Direction dir) { // fare più pulito
            foreach (DirectionEnum dEnum in _availableJointDirections) {
                if ((dir - _facingDirection).Equals(dEnum)) return true;
            }
            return false;
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