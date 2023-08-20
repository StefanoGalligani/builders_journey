using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    [Serializable]
    public struct PieceDataSerializable
    {
        public int pieceId;
        public int[] pieceCoordinates;
        public int pieceRotation;
        public string binding;
    }
}