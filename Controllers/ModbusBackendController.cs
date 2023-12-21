using Microsoft.AspNetCore.Mvc;
using ModbusBackend.Models;
using ModbusBackend.Repository;
using NModbus;
using System.ComponentModel;
using System.Net.Sockets;

namespace ModbusBackend.Controllers
{
    [Route("/api/")]
    [ApiController]
    public class ModbusBackendController : ControllerBase
    {
        [HttpGet("getMatismartRecloserStatusRaw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MatismartRecloserStatusModel> GetMatismartRecloserStatusRaw([DefaultValue(Server.EportService)] Server server, byte slaveId)
        {
            MatismartRequestRepository ModbusRequestRepository = new(server);
            try
            {
                using TcpClient client = new(ModbusRequestRepository.IpAddress, ModbusRequestRepository.Port);
                try
                {
                    var master = ModbusRequestRepository.InitializeModbusMaster(client);
                    ushort[] registers = master.ReadHoldingRegisters(slaveId, 15, 1);
                    if ((registers != null) && (registers.Length == 1))
                    {
                        if ((registers[0] == 3) || (registers[0] == 6))
                        {
                            var model = new MatismartRecloserStatusModel
                            {
                                IpAddress = ModbusRequestRepository.IpAddress,
                                Port = ModbusRequestRepository.Port,
                                SlaveId = slaveId,
                                IsOpened = registers[0] == 6,
                                Reference = "status read successfull"
                            };
                            return Ok(model);
                        }
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("setMatismartRecloserStatusRaw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> SetMatismartRecloserStatusRaw([DefaultValue(Server.EportService)] Server server, byte slaveId, [DefaultValue(true)] bool isOpened)
        {
            MatismartRequestRepository ModbusRequestRepository = new(server);
            try
            {
                using TcpClient client = new(ModbusRequestRepository.IpAddress, ModbusRequestRepository.Port);
                try
                {
                    var master = ModbusRequestRepository.InitializeModbusMaster(client);
                    master.WriteSingleRegister(slaveId, 17, (ushort)(isOpened ? 2 : 1));

                    var model = new MatismartRecloserStatusModel
                    {
                        IpAddress = ModbusRequestRepository.IpAddress,
                        Port = ModbusRequestRepository.Port,
                        SlaveId = slaveId,
                        IsOpened = isOpened,
                        Reference = (isOpened ? "turn on" : "turn off") + " was successful"
                    };
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
