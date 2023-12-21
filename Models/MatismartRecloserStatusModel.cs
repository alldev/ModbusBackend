namespace ModbusBackend.Models
{
    public class MatismartRecloserStatusModel
    {
        public required string IpAddress { get; set; }
        public required int Port { get; set; }
        public required byte SlaveId { get; set; }
        public required bool IsOpened { get; set; }
        public required string Reference { get; set; }
    }
}
