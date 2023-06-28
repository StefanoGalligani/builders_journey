using UnityEngine;
using BuilderGame.BuildingPhase.Grid;
using BuilderGame.BuildingPhase.Builder.FileManagement;

namespace BuilderGame.BuildingPhase.Builder {
    public class BuilderManager
    {
        private Vehicle _vehicle;
        private Piece _piecePrefab;
        private int _pieceId;
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
            
            _vehicleConnectionManager = new VehicleConnectionManager(gridInfo, vehicle);

            bool vehicleBuilt = false;
            if (VehicleFileManagerSingleton.Instance.IsVehicleSaved()) {
                vehicleBuilt = BuildVehicleFromData(VehicleFileManagerSingleton.Instance.GetVehicleData(), mainPiecePrefab);
            }
            if (!vehicleBuilt) {
                PlaceMainPiece(mainPiecePrefab);
            }

        }

        private void PlaceMainPiece(Piece mainPiecePrefab) {
            int x = _gridInfo.MainPieceCoordinates.x;
            int y = _gridInfo.MainPieceCoordinates.y;
            InstantiateNewPiece(mainPiecePrefab, x, y, 0, true);
        }

        public void SetPiecePrefab(Piece piecePrefab, int pieceId) {
            _piecePrefab = piecePrefab;
            _pieceId = pieceId;
        }

        public void PlacePiece(Vector2Int gridCoords) {
            if (!IsPlaceable(gridCoords)) return;

            if (_placedPieces[gridCoords.x][gridCoords.y] != null) {
                _placedPieces[gridCoords.x][gridCoords.y].transform.SetParent(null);
                GameObject.Destroy(_placedPieces[gridCoords.x][gridCoords.y].gameObject);
                _placedPieces[gridCoords.x][gridCoords.y] = null;
            }
            
            if (_piecePrefab != null) {
                InstantiateNewPiece(_piecePrefab, gridCoords.x, gridCoords.y, _pieceId);
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

        private bool BuildVehicleFromData(VehicleDataSerializable vehicleData, Piece mainPiecePrefab) {
            PiecesDictionary piecesDictionary = GameObject.FindObjectOfType<PiecesDictionary>();
            if (!piecesDictionary.AreAllIdsValid(vehicleData.pieceIds)) {
                Debug.LogWarning("The loaded vehicle contains pieces that are not available in this level");
                return false;
            }
            //controllare che le coordinate dei pezzi siano valide, shiftare se si possono adattare, altrimenti return false
            for (int i=0; i<vehicleData.pieceIds.Length; i++) {
                int id = vehicleData.pieceIds[i];
                Piece prefab = (id>0) ? piecesDictionary.GetPrefabById(id) : mainPiecePrefab;
                int[] coords = vehicleData.pieceCoordinates[i];
                Piece newPiece = InstantiateNewPiece(prefab, coords[0], coords[1], id, id==0);
                newPiece.SetRotation(vehicleData.pieceRotations[i]);
            }
            _vehicle.IsReadyToStart = _vehicleConnectionManager.ConnectPieces(_placedPieces);
            return true;
        }

        private Piece InstantiateNewPiece(Piece prefab, int gridx, int gridy, int id, bool isMain=false) {
            Piece newPiece = GameObject.Instantiate(prefab, _vehicle.transform);
            newPiece.transform.position = PositionFromGridCoordinates(gridx, gridy);
            _placedPieces[gridx][gridy] = newPiece;

            newPiece.Init(new Vector2Int(gridx, gridy), id, isMain);

            return newPiece;
        }

        private Vector2 PositionFromGridCoordinates(int gridx, int gridy) {
            return _gridInfo.BottomLeftCoords + new Vector2(gridx,gridy) * _gridInfo.CellSize + _gridInfo.GridOffset;
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