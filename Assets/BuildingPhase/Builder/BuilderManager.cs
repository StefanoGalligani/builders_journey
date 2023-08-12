using UnityEngine;
using BuilderGame.BuildingPhase.Grid;
using BuilderGame.BuildingPhase.Price;
using BuilderGame.BuildingPhase.Builder.FileManagement;

namespace BuilderGame.BuildingPhase.Builder {
    public class BuilderManager
    {
        public int NewPieceId {private get; set;}
        private Vehicle _vehicle;
        private Piece[][] _placedPieces;
        private GridInfoScriptableObject _gridInfo;
        private VehicleConnectionManager _vehicleConnectionManager;
        private PiecesDictionary _piecesDictionary;
        private TotalPriceInfo _totalPriceInfo;

        public BuilderManager(GridInfoScriptableObject gridInfo, Vehicle vehicle) {
            _gridInfo = gridInfo;
            _vehicle = vehicle;

            _placedPieces = new Piece[gridInfo.GridDimensions.x][];
            for (int i=0; i<_placedPieces.Length; i++) {
                _placedPieces[i] = new Piece[gridInfo.GridDimensions.y];
            }
            
            _vehicleConnectionManager = new VehicleConnectionManager(gridInfo, vehicle);
            _piecesDictionary = GameObject.FindObjectOfType<PiecesDictionary>();

            bool vehicleBuilt = false;
            if (VehicleFileManagerSingleton.Instance.IsVehicleSaved()) {
                vehicleBuilt = BuildVehicleFromData(VehicleFileManagerSingleton.Instance.GetVehicleData());
            }
            if (!vehicleBuilt) {
                PlaceMainPiece();
            }

        }

        private void PlaceMainPiece() {
            int x = _gridInfo.MainPieceCoordinates.x;
            int y = _gridInfo.MainPieceCoordinates.y;
            InstantiateNewPiece(0, _gridInfo.MainPieceCoordinates);
        }

        public void PlacePiece(Vector2Int gridCoords) {
            if (!IsPlaceable(gridCoords)) return;

            if (_placedPieces[gridCoords.x][gridCoords.y] != null) {
                RemovePiece(gridCoords);
            }
            
            if (NewPieceId >= 0) {
                InstantiateNewPiece(NewPieceId, gridCoords);
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

        private bool BuildVehicleFromData(VehicleDataSerializable vehicleData) {
            if (!_piecesDictionary.AreAllIdsValid(vehicleData.pieceIds)) {
                Debug.LogWarning("The loaded vehicle contains pieces that are not available in this level");
                return false;
            }
            //controllare che le coordinate dei pezzi siano valide, shiftare se si possono adattare, altrimenti return false
            for (int i=0; i<vehicleData.pieceIds.Length; i++) {
                int id = vehicleData.pieceIds[i];
                Piece prefab = _piecesDictionary.GetPrefabById(id);
                int[] coords = vehicleData.pieceCoordinates[i];
                Piece newPiece = InstantiateNewPiece(id, new Vector2Int(coords[0], coords[1]));
                newPiece.SetRotation(vehicleData.pieceRotations[i]);
            }
            _vehicle.IsReadyToStart = _vehicleConnectionManager.ConnectPieces(_placedPieces);
            return true;
        }

        private void RemovePiece(Vector2Int gridCoords) {
            int price = _piecesDictionary.GetPriceById(_placedPieces[gridCoords.x][gridCoords.y].Id);
            _placedPieces[gridCoords.x][gridCoords.y].transform.SetParent(null);
            GameObject.Destroy(_placedPieces[gridCoords.x][gridCoords.y].gameObject);
            _placedPieces[gridCoords.x][gridCoords.y] = null;

            _totalPriceInfo.SubtractPrice(price);
        }

        private Piece InstantiateNewPiece(int id, Vector2Int gridCoords) {
            int price = _piecesDictionary.GetPriceById(id);
            Piece prefab = _piecesDictionary.GetPrefabById(id);

            Piece newPiece = GameObject.Instantiate(prefab, _vehicle.transform);
            newPiece.transform.position = PositionFromGridCoordinates(gridCoords);
            _placedPieces[gridCoords.x][gridCoords.y] = newPiece;

            bool isMain = id==0;
            newPiece.Init(id, gridCoords, isMain);

            _totalPriceInfo.SumPrice(price);

            return newPiece;
        }

        private Vector2 PositionFromGridCoordinates(Vector2Int gridCoords) {
            return _gridInfo.BottomLeftCoords + new Vector2(gridCoords.x, gridCoords.y) * _gridInfo.CellSize + _gridInfo.GridOffset;
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