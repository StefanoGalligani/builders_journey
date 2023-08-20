using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.VehicleManagement;
using BuilderGame.Utils;
using System.Linq;

namespace BuilderGame.BuildingPhase.Builder {
    internal class VehicleConnectionManager {
        private Vehicle _vehicle;

        internal VehicleConnectionManager(Vehicle vehicle) {
            _vehicle = vehicle;
        }

        internal bool ConnectPieces(Piece[][] placedPieces, Vector2Int mainPieceCoords) {
            Piece[] pieces = _vehicle.GetComponentsInChildren<Piece>();
            if(pieces.Length < 2) return false;

            pieces.ToList().ForEach(p => p.DetachJoint());

            Queue<Piece> pieceQueue = new Queue<Piece>();
            pieceQueue.Enqueue(placedPieces[mainPieceCoords.x][mainPieceCoords.y]);
            
            while (pieceQueue.Count > 0) {
                Piece currentPiece = pieceQueue.Dequeue();
                if (currentPiece.CanBeAttachedTo) {
                    for (int i=0; i<4; i++) {
                        Direction d = i;
                        int newX = ((Vector3Int)d).x + currentPiece.GridPosition.x;
                        int newY = ((Vector3Int)d).y + currentPiece.GridPosition.y;
                        if (!UtilsFunctions.IsValidPosition(placedPieces, newX, newY)) continue;

                        Piece neighbour = placedPieces[newX][newY];

                        if (neighbour != null && !pieceQueue.Contains(neighbour) && !neighbour.IsConnected){
                            if (neighbour.IsPossibleJointDirection(d+2)) {
                                neighbour.ConnectJoint(currentPiece.GetComponent<Rigidbody2D>(), d+2);
                                pieceQueue.Enqueue(neighbour);
                            }
                        }
                    }
                }
            }

            Piece notConnected = pieces.ToList().FirstOrDefault(p => !p.IsConnected);

            return notConnected == null;
        }
    }
}