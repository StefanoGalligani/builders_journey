using System;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    [Serializable]
    public class VehicleDataSerializable
    {
        public int[] pieceIds;
        public int[][] pieceCoordinates;
        public int[] pieceRotations;
        public string[] rebinds;
    }
}
