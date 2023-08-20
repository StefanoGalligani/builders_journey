using System;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement.FileManagement
{
    [Serializable]
    public class VehicleDataSerializable
    {
        public PieceDataSerializable[] data;
        public int[] GetAllIds() {
            int[] ids = new int[data.Length];
            for (int i=0; i<data.Length; i++) {
                ids[i] = data[i].pieceId;
            }
            return ids;
        }
    }
}
