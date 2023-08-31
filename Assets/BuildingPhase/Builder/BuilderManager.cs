using UnityEngine;
using BuilderGame.BuildingPhase.Dictionary;
using BuilderGame.BuildingPhase.Price;
using BuilderGame.BuildingPhase.Start;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement;
using BuilderGame.Utils;
using BuilderGame.Pieces;
using UnityEngine.SceneManagement;

namespace BuilderGame.BuildingPhase.Builder {
    internal class BuilderManager
    {
        internal int NewPieceId {private get{return _newPieceId;} set {_newPieceId = value; _validSelection = true;}}
        private int _newPieceId;
        private Vehicle _vehicle;
        private Piece[][] _placedPieces;
        private GridInfoScriptableObject _gridInfo;
        private VehicleConnector _vehicleConnectionManager;
        private PiecesDictionary _piecesDictionary;
        private TotalPriceInfo _totalPriceInfo;
        private bool _validSelection = false;
        private Vector2Int _mainPieceCoords;

        internal BuilderManager(GridInfoScriptableObject gridInfo, Vehicle vehicle) {
            _gridInfo = gridInfo;
            _vehicle = vehicle;

            _placedPieces = new Piece[gridInfo.GridDimensions.x][];
            for (int i=0; i<_placedPieces.Length; i++) {
                _placedPieces[i] = new Piece[gridInfo.GridDimensions.y];
            }
            
            _vehicleConnectionManager = new VehicleConnector(vehicle);
            _piecesDictionary = GameObject.FindObjectOfType<PiecesDictionary>();
            _totalPriceInfo = GameObject.FindObjectOfType<TotalPriceInfo>();

            bool vehicleBuilt = false;
            if (VehicleFileAccessSingleton.Instance.IsVehicleSaved()) {
                vehicleBuilt = BuildVehicleFromData(VehicleFileAccessSingleton.Instance.GetVehicleData());
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

        internal void PlacePiece(Vector2Int gridCoords) {
            if (!IsPlaceable(gridCoords)) return;

            if (_placedPieces[gridCoords.x][gridCoords.y] != null) {
                RemovePiece(gridCoords);
            }
            
            if (NewPieceId >= 0) {
                Piece p = InstantiateNewPiece(NewPieceId, gridCoords);
            }
            
            GameObject.FindObjectOfType<StartNotifier>().CanStart = _vehicleConnectionManager.ConnectPieces(_placedPieces, _mainPieceCoords);
        }

        internal Piece GetPieceAtPosition(Vector2Int gridCoords) {
            return _placedPieces[gridCoords.x][gridCoords.y];
        }

        internal void RotatePiece(Vector2Int gridCoords) {
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
                    if (!UtilsFunctions.IsValidPosition(_placedPieces, newX, newY)) continue;

                    if (_placedPieces[newX][newY] != null)
                        return true;
                }
            }
            return false;
        }

        private bool BuildVehicleFromData(VehicleDataSerializable vehicleData) {
            if (!_piecesDictionary.AreAllIdsValid(vehicleData.GetAllIds())) {
                Debug.LogWarning("The loaded vehicle contains pieces that are not available in this level");
                return false;
            }
            Vector2Int maxCoord = MaximumCoord(vehicleData);
            Vector2Int necessaryShift = _gridInfo.GridDimensions - maxCoord - new Vector2Int(1,1);
            if (necessaryShift.x < 0 || necessaryShift.y < 0) {
                Vector2Int vehicleSize = maxCoord - MinimumCoord(vehicleData) + new Vector2Int(1,1);
                if (vehicleSize.x > _gridInfo.GridDimensions.x || vehicleSize.y > _gridInfo.GridDimensions.y) {
                    Debug.LogWarning("The loaded vehicle is too large for this level");
                    return false;
                }
                for (int i=0; i<vehicleData.data.Length; i++) {
                    vehicleData.data[i].pieceCoordinates[0] += necessaryShift.x;
                    vehicleData.data[i].pieceCoordinates[1] += necessaryShift.y;
                }
            }
            for (int i=0; i<vehicleData.data.Length; i++) {
                int id = vehicleData.data[i].pieceId;
                Piece prefab = _piecesDictionary.GetPrefabById(id);
                int[] coords = vehicleData.data[i].pieceCoordinates;
                Piece newPiece = InstantiateNewPiece(id, new Vector2Int(coords[0], coords[1]));
                newPiece.SetRotation(vehicleData.data[i].pieceRotation);
                if (id == 0) _mainPieceCoords = newPiece.GridPosition;
                SpecialPiece sp = newPiece.gameObject.GetComponent<SpecialPiece>();
                if (sp && vehicleData.data[i].binding.Length > 0) {
                    sp.LoadBindingJson(vehicleData.data[i].binding);
                }
            }
            GameObject.FindObjectOfType<StartNotifier>().CanStart = _vehicleConnectionManager.ConnectPieces(_placedPieces, _mainPieceCoords);
            return true;
        }
        
        private Vector2Int MaximumCoord(VehicleDataSerializable vehicleData) {
            Vector2Int maxCoord = new Vector2Int(0, 0);
            for (int i=0; i<vehicleData.data.Length; i++) {
                int[] coords = vehicleData.data[i].pieceCoordinates;
                if (coords[0] > maxCoord.x) maxCoord.x = coords[0];
                if (coords[1] > maxCoord.y) maxCoord.y = coords[1];
            }
            return maxCoord;
        }
        
        private Vector2Int MinimumCoord(VehicleDataSerializable vehicleData) {
            Vector2Int minCoord = new Vector2Int(1000, 1000);
            for (int i=0; i<vehicleData.data.Length; i++) {
                int[] coords = vehicleData.data[i].pieceCoordinates;
                if (coords[0] < minCoord.x) minCoord.x = coords[0];
                if (coords[1] < minCoord.y) minCoord.y = coords[1];
            }
            return minCoord;
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
    }
}