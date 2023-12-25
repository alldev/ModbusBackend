using ModbusBackend.Models;
using System.Xml.Linq;
using static ModbusBackend.Repository.IMatismartItemsRepository;

namespace ModbusBackend.Repository
{
    public interface IMatismartItemsRepository
    {
        public abstract List<MatismartRecloserItemModel> items
        {
            get;
            set;
        }
    }

    public class MatismartItemsRepository: IMatismartItemsRepository
    {
        private List<MatismartRecloserItemModel> _items = new();

        List<MatismartRecloserItemModel> IMatismartItemsRepository.items {
            get => _items;
            set => _items = value;
        }

        public enum Server
        {
            EportService,
            EportServer,
            EportPhase1,
            EportPhase2,
            EportPhase3
        }

        public string GetIpAddress(Server server)
        {
            return server switch
            {
                Server.EportService => "192.168.1.250",
                Server.EportServer => "192.168.1.251",
                Server.EportPhase1 => "192.168.1.252",
                Server.EportPhase2 => "192.168.1.253",
                Server.EportPhase3 => "192.168.1.254",
                _ => "0.0.0.0",
            };
        }

        public MatismartItemsRepository()
        {
            _items.Add(new MatismartRecloserItemModel(201, GetIpAddress(Server.EportService), 8899, "", "УЗО розетки", null, RecloserType.RCB, RecloserControlOver.Sockets, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(202, GetIpAddress(Server.EportService), 8899, "", "УЗО освещение", null, RecloserType.RCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(116, GetIpAddress(Server.EportService), 8899, "", "Освещение: гараж и вход", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(117, GetIpAddress(Server.EportService), 8899, "", "Освещение: лестница и холл", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(118, GetIpAddress(Server.EportService), 8899, "", "Освещение: туалет и вентилляция", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(119, GetIpAddress(Server.EportService), 8899, "", "Освещение: маленькая комната", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(120, GetIpAddress(Server.EportService), 8899, "", "Освещение: гостиная и кухня", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(111, GetIpAddress(Server.EportService), 8899, "", "Розетки: маленькая комната и туалет", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(112, GetIpAddress(Server.EportService), 8899, "", "Розетки: кухня и гостиная 1", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(113, GetIpAddress(Server.EportService), 8899, "", "Розетки: кухня и гостиная 2", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(114, GetIpAddress(Server.EportService), 8899, "", "Розетки: домофон и мониторы", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
            _items.Add(new MatismartRecloserItemModel(115, GetIpAddress(Server.EportService), 8899, "", "Розетки: вытяжка и кухня", null, RecloserType.MCB, RecloserControlOver.Light, 0, RecloserStatus.Disabled));
        }
    }
}
