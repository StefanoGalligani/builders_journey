using UnityEngine;
using BuilderGame.BuildingPhase.Grid;
using Cinemachine;

namespace BuilderGame.BuildingPhase.Builder {
    public class BuilderManager
    {
        private Vehicle _vehicle;
        private Piece _piecePrefab;
        private Piece[][] _placedPieces;
        private GridInfoScriptableObject _gridInfo;
        private VehicleConnectionManager _vehicleConnectionManager;

        public BuilderManager(GridInfoScriptableObject gridInfo, Vehicle vehicle, Piece mainPiecePrefab) {
            _gridInfo = gridInfo;
            _vehicle = vehicle;

            _placedPieces = new Piece[gridInfo.GridDimensions.x][];
            for (int i=0; i<_placedPieces.Length; i++) {
                _placedPieces[i] = new Piece[gridInfo.GridDimensions.y];
            }

            PlaceMainPiece(mainPiecePrefab);

            _vehicleConnectionManager = new VehicleConnectionManager(gridInfo, vehicle);
        }

        private void PlaceMainPiece(Piece mainPiecePrefab) {
            int x = _gridInfo.MainPieceCoordinates.x;
            int y = _gridInfo.MainPieceCoordinates.y;
            _placedPieces[x][y] = GameObject.Instantiate(mainPiecePrefab, _vehicle.transform);
            _placedPieces[x][y].transform.position = _gridInfo.BottomLeftCoords + new Vector2(x, y) * _gridInfo.CellSize + _gridInfo.GridOffset;

            _placedPieces[x][y].Init(_gridInfo.MainPieceCoordinates, true);
            GameObject.FindObjectOfType<CinemachineVirtualCamera>().Follow = _placedPieces[x][y].transform; //gestire la cosa in modo diverso
        }

        public void SetPiecePrefab(Piece piecePrefab) {
            _piecePrefab = piecePrefab;
        }

        public void PlacePiece(Vector2Int gridCoords) {
            if (!IsPlaceable(gridCoords)) return;

            if (_placedPieces[gridCoords.x][gridCoords.y] != null) {
                _placedPieces[gridCoords.x][gridCoords.y].transform.SetParent(null);
                GameObject.Destroy(_placedPieces[gridCoords.x][gridCoords.y].gameObject);
                _placedPieces[gridCoords.x][gridCoords.y] = null;
            }
            
            if (_piecePrefab != null) {
                Piece newBlock = GameObject.Instantiate<Piece>(_piecePrefab);
                newBlock.transform.position = _gridInfo.BottomLeftCoords + new Vector2(gridCoords.x, gridCoords.y) * _gridInfo.CellSize + _gridInfo.GridOffset;
                newBlock.transform.SetParent(_vehicle.transform);
                _placedPieces[gridCoords.x][gridCoords.y] = newBlock;

                newBlock.Init(gridCoords);
            }
            
            _vehicle.IsReadyToStart = _vehicleConnectionManager.ConnectPieces(_placedPieces);
        }

        public void RotatePiece(Vector2Int gridCoords) {
            if (_placedPieces[gridCoords.x][gridCoords.y] == null) {
                return;
            }
            _placedPieces[gridCoords.x][gridCoords.y].Rotate();
        }

        private bool IsPlaceable(Vector2Int gridCoords) {
            if (gridCoords.x == _gridInfo.MainPieceCoordinates.x && gridCoords.y == _gridInfo.MainPieceCoordinates.y)
                return false;
            for (int i=-1; i<=1; i++) {
                for (int j=-1; j<=1; j++) {
                    if (i*j != 0) continue;
                    
                    int newX = gridCoords.x + i;
                    int newY = gridCoords.y + j;
                    if (!IsValidPosition(newX, newY)) continue;

                    if (_placedPieces[newX][newY] != null)
                        return true;
                }
            }
            return false;
        }

        private bool IsValidPosition(int x, int y) { //estrarre in una classe di utils
            if (x < 0 || x >= _placedPieces.Length)
                return false;
            if (y < 0 || y >= _placedPieces[x].Length)
                return false;
            return true;
        }
    }
}