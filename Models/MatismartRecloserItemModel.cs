using System.ComponentModel.DataAnnotations;

namespace ModbusBackend.Models
{
    public enum RecloserType
    {
        MCB, // Miniature Circuit Breaker
        RCB, // Residual Current Device
    }

    public enum RecloserControlOver
    {
        Light,
        Sockets,
        Line
    }

    public class MatismartRecloserItemModel
    {
        public byte SlaveId;
        public string Comment;
        public RecloserType Type;
        public RecloserControlOver ControlOver;
        public MatismartRecloserItemModel? Parent;
        public byte GroupID;

        public MatismartRecloserItemModel(byte slaveId, string comment, MatismartRecloserItemModel? parent,
            RecloserType type, RecloserControlOver controlOver, byte groupID)
        {
            SlaveId = slaveId;
            Comment = comment;
            Parent = parent;
            Type = type;
            ControlOver = controlOver;
            GroupID = groupID;
        }
    }
}
