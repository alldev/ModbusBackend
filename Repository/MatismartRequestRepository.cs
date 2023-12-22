using Microsoft.AspNetCore.Mvc;
using ModbusBackend.Models;
using NModbus;
using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace ModbusBackend.Repository
{

    public class MatismartRequestRepository
    {
        public MatismartRequestRepository(){}

        public IModbusMaster InitializeModbusMaster(TcpClient client)
        {
            var factory = new ModbusFactory();
            IModbusMaster master = factory.CreateMaster(client);
            master.Transport.Retries = 0;
            master.Transport.ReadTimeout = 3000;
            master.Transport.WriteTimeout = 3000;
            return master;
        }
    }
}
