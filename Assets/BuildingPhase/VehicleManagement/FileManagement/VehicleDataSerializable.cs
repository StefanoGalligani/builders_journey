using System;

namespace BuilderGame.BuildingPhase.VehicleManagement.FileManagement
{
    [Serializable]
    public class VehicleDataSerializable
    {
        public int[] pieceIds;
        public int[][] pieceCoordinates;
        public int[] pieceRotations;
    }
}
