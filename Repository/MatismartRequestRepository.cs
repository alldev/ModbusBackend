using Microsoft.AspNetCore.Mvc;
using ModbusBackend.Models;
using NModbus;
using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace ModbusBackend.Repository
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Server
    {
        EportService,
        EportServer,
        EportPhase1,
        EportPhase2,
        EportPhase3
    }


    public class MatismartRequestRepository
    {
        public readonly int Port = 8899;
        public Server Server { get; }

        public MatismartRequestRepository(Server server)
        {
            Server = server;
        }

        public string IpAddress
        {
            get
            {
                return Server switch
                {
                    Server.EportService => "192.168.1.250",
                    Server.EportServer => "192.168.1.251",
                    Server.EportPhase1 => "192.168.1.252",
                    Server.EportPhase2 => "192.168.1.253",
                    Server.EportPhase3 => "192.168.1.254",
                    _ => "0.0.0.0",
                };
            }
        }

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
