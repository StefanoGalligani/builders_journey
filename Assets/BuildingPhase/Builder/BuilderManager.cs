using UnityEngine;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.Price;
using BuilderGame.Utils;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;

namespace BuilderGame.BuildingPhase.Builder {
    public class BuilderManager
    {
        public int NewPieceId {private get{return _newPieceId;} set {_newPieceId = value; _validSelection = true;}}
        private int _newPieceId;
        private Vehicle _vehicle;
        private Piece[][] _placedPieces;
        private GridInfoScriptableObject _gridInfo;
        private VehicleConnectionManager _vehicleConnectionManager;
        private PiecesDictionary _piecesDictionary;
        private TotalPriceInfo _totalPriceInfo;
        private bool _validSelection = false;
        private Vector2Int _mainPieceCoords;

        public BuilderManager(GridInfoScriptableObject gridInfo, Vehicle vehicle) {
            _gridInfo = gridInfo;
            _vehicle = vehicle;

            _placedPieces = new Piece[gridInfo.GridDimensions.x][];
            for (int i=0; i<_placedPieces.Length; i++) {
                _placedPieces[i] = new Piece[gridInfo.GridDimensions.y];
            }
            
            _vehicleConnectionManager = new VehicleConnectionManager(vehicle);
            _piecesDictionary = GameObject.FindObjectOfType<PiecesDictionary>();
            _totalPriceInfo = GameObject.FindObjectOfType<TotalPriceInfo>();

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
            _mainPieceCoords = _gridInfo.MainPieceCoordinates;
        }

        public void PlacePiece(Vector2Int gridCoords) {
            if (!IsPlaceable(gridCoords)) return;

            if (_placedPieces[gridCoords.x][gridCoords.y] != null) {
                RemovePiece(gridCoords);
            }
            
            if (NewPieceId >= 0) {
                InstantiateNewPiece(NewPieceId, gridCoords);
            }
            
            _vehicle.IsReadyToStart = _vehicleConnectionManager.ConnectPieces(_placedPieces, _mainPieceCoords);
        }

        public void RotatePiece(Vector2Int gridCoords) {
            if (_placedPieces[gridCoords.x][gridCoords.y] == null) {
                return;
            }
            _placedPieces[gridCoords.x][gridCoords.y].Rotate();
        }

        private bool IsPlaceable(Vector2Int gridCoords) {
            if (!_validSelection) return false;
            if (gridCoords.x == _mainPieceCoords.x && gridCoords.y == _mainPieceCoords.y)
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
                if (id == 0) _mainPieceCoords = newPiece.GridPosition;
            }
            _vehicle.IsReadyToStart = _vehicleConnectionManager.ConnectPieces(_placedPieces, _mainPieceCoords);
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

        internal void ShiftAllPieces(Direction dir) {
            bool shiftIsValid = true;
            switch(dir) {
                case Direction.Right:
                    int i=_gridInfo.GridDimensions.x-1;
                    for (int j=0; j<_gridInfo.GridDimensions.y; j++) {
                        if (_placedPieces[i][j]) {
                            shiftIsValid = false;
                            break;
                        }
                    }
                break;
                case Direction.Up:
                    int j1=_gridInfo.GridDimensions.y-1;
                    for (i=0; i<_gridInfo.GridDimensions.x; i++) {
                        if (_placedPieces[i][j1]) {
                            shiftIsValid = false;
                            break;
                        }
                    }
                break;
                case Direction.Left:
                    int i1=0;
                    for (int j=0; j<_gridInfo.GridDimensions.y; j++) {
                        if (_placedPieces[i1][j]) {
                            shiftIsValid = false;
                            break;
                        }
                    }
                break;
                case Direction.Down:
                    int j2=0;
                    for (i=0; i<_gridInfo.GridDimensions.x; i++) {
                        if (_placedPieces[i][j2]) {
                            shiftIsValid = false;
                            break;
                        }
                    }
                break;
                default:
                    shiftIsValid = false;
                break;
            }
            if (!shiftIsValid) {
                Debug.LogWarning("Pieces cannot be shifted in direction " + ((Vector2)dir).x + "," + ((Vector2)dir).y);
                return;
            }
            _mainPieceCoords.x += ((Vector2Int)dir).x;
            _mainPieceCoords.y += ((Vector2Int)dir).y;
            switch(dir) {
                case Direction.Right:
                    for (int i=_gridInfo.GridDimensions.x-2; i>=0; i--) {
                        for (int j=0; j<_gridInfo.GridDimensions.y; j++) {
                            ShiftPiece(new Vector2Int(i, j), dir);
                        }
                    }
                break;
                case Direction.Up:
                    for (int j=_gridInfo.GridDimensions.y-2; j>=0; j--) {
                        for (int i=_gridInfo.GridDimensions.x-1; i>=0; i--) {
                            ShiftPiece(new Vector2Int(i, j), dir);
                        }
                    }
                break;
                case Direction.Left:
                    for (int i=1; i<_gridInfo.GridDimensions.x; i++) {
                        for (int j=0; j<_gridInfo.GridDimensions.y; j++) {
                            ShiftPiece(new Vector2Int(i, j), dir);
                        }
                    }
                break;
                case Direction.Down:
                    for (int j=1; j<_gridInfo.GridDimensions.y; j++) {
                        for (int i=0; i<_gridInfo.GridDimensions.x; i++) {
                            ShiftPiece(new Vector2Int(i, j), dir);
                        }
                    }
                break;
            }
        }

        private void ShiftPiece(Vector2Int gridCoords, Direction dir) {
            Piece p = _placedPieces[gridCoords.x][gridCoords.y];
            if (p != null) {
                p.Shift(dir, _gridInfo.CellSize);
                Vector2Int offset = dir;
                _placedPieces[gridCoords.x + offset.x][gridCoords.y + offset.y] = p;
                _placedPieces[gridCoords.x][gridCoords.y] = null;
                
            }
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