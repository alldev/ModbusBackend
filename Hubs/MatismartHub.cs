using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ModbusBackend.Repository;

namespace ModbusBackend.Hubs
{
    public class MatismartHub : Hub
    {
        public IMatismartItemsRepository _matismartItemsRepository;

        public async Task Send()
        {
            foreach (var item in _matismartItemsRepository.items)
            {
                await Clients.All.SendAsync("Receive", item.DivName, item.IsOpened, item.IsLoading);
            }
        }

        public MatismartHub(IMatismartItemsRepository matismartItemsRepository)
        {
            _matismartItemsRepository = matismartItemsRepository;
        }
    }
}
