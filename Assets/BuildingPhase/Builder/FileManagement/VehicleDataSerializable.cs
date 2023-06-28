using System;

namespace BuilderGame.BuildingPhase.Builder.FileManagement
{
    [Serializable]
    internal class VehicleDataSerializable
    {
        public int[] pieceIds;
        public int[][] pieceCoordinates;
        public int[] pieceRotations;
    }
}
