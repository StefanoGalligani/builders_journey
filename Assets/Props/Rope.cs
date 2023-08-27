using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Props
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private bool _createOnStart = true;
        [SerializeField] private Joint2D _ropeStartPrefab;
        [SerializeField] private Joint2D _ropeChildPrefab;
        [SerializeField] private Joint2D _ropeEndPrefab;
        [SerializeField] private Vector2 _ropeDirection;
        [SerializeField] private Rigidbody2D _attachedObject;
        [SerializeField] private Vector2 _objectOffset;
        [SerializeField] private int _ropeLength;
        [SerializeField] private float _pieceLength;

        private Joint2D[] _ropePieces;
        private LineRenderer _lineRenderer;

        private void Start() {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = _ropeLength+1;
            if (_createOnStart) {
                _ropePieces = new Joint2D[_ropeLength + 1];
                CreateRope();
                AdaptObject();
            } else {
                _ropePieces = transform.GetComponentsInChildren<Joint2D>();
            }
        }

        private void CreateRope() {
            _ropePieces[0] = Instantiate(_ropeStartPrefab, transform);
            _ropePieces[0].transform.position = transform.position;

            for (int i=1; i<_ropeLength; i++) {
                _ropePieces[i] = Instantiate(_ropeChildPrefab, transform);
                _ropePieces[i].transform.position = transform.position + (Vector3)_ropeDirection.normalized * i * _pieceLength;
                _ropePieces[i-1].connectedBody = _ropePieces[i].GetComponent<Rigidbody2D>();
            }

            _ropePieces[_ropeLength] = Instantiate(_ropeEndPrefab, transform);
            _ropePieces[_ropeLength].transform.position = transform.position + (Vector3)_ropeDirection.normalized * _ropeLength * _pieceLength;
            _ropePieces[_ropeLength-1].connectedBody = _ropePieces[_ropeLength].GetComponent<Rigidbody2D>();
            _ropePieces[_ropeLength].connectedBody = _attachedObject;
        }

        private void AdaptObject() {
            Transform t = _attachedObject.transform;
            t.position = _ropePieces[_ropeLength].transform.position - (Vector3)_objectOffset;
        }

        private void Update() {
            Vector3[] positions = _ropePieces.AsEnumerable().Select(x => x.transform.position).ToArray();
            _lineRenderer.SetPositions(positions);
        }
    }
}
