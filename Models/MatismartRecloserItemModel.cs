using System.ComponentModel.DataAnnotations;
using static ModbusBackend.Repository.MatismartItemsRepository;

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

    public enum RecloserStatus
    {
        TurnedOn,
        TurnedOff,
        Disabled
    }

    public class MatismartRecloserItemModel
    {
        public byte SlaveId;
        public string NameEnglish;
        public string NameOtherLanguage;
        public string IpAddress;
        public int Port;
        public RecloserType Type;
        public RecloserControlOver ControlOver;
        public MatismartRecloserItemModel? Parent;
        public byte GroupID;
        public RecloserStatus Status;
        public string DivName;

        public bool IsOpened
        {
            get
            {
                if (Status == RecloserStatus.TurnedOn) return true;
                return false;
            }
        }

        public bool IsLoading
        {
            get
            {
                if (Status == RecloserStatus.Disabled) return true;
                return false;
            }
        }

        public bool IsMCB
        {
            get
            {
                if (Type == RecloserType.MCB) return true;
                return false;
            }
        }

        public bool IsRCB
        {
            get
            {
                if (Type == RecloserType.RCB) return true;
                return false;
            }
        }

        public MatismartRecloserItemModel(byte slaveId, string ipAddress, int port, string nameEnglish, string nameOtherLanguage, MatismartRecloserItemModel? parent,
            RecloserType type, RecloserControlOver controlOver, byte groupID, RecloserStatus status)
        {
            SlaveId = slaveId;
            IpAddress = ipAddress;
            Port = port;
            NameEnglish = nameEnglish;
            NameOtherLanguage = nameOtherLanguage;
            Parent = parent;
            Type = type;
            ControlOver = controlOver;
            GroupID = groupID;
            Status = status;
            DivName = "item-" + slaveId.ToString();
        }
    }
}
