using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModbusBackend.Models;
using ModbusBackend.Repository;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace ModbusWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IMatismartItemsRepository _matismartItemsRepository;

        public IndexModel(ILogger<IndexModel> logger, IMatismartItemsRepository matismartItemsRepository)
        {
            _logger = logger;
            _matismartItemsRepository = matismartItemsRepository;
        }

        public void OnGet()
        {
            Task.Factory.StartNew(() =>
           {
               while (true)
               {
                   foreach (var item in _matismartItemsRepository.items)
                   {
                       HttpClient client = new();
                       var request = "http://localhost/api/getMatismartRecloserStatus?server=" + item.IpAddress + "&port=" + item.Port.ToString() + "&slaveId=" + item.SlaveId.ToString();
                       // Console.WriteLine(request);
                       client.GetAsync(request).ContinueWith(
                           (requestTask) =>
                           {
                               HttpResponseMessage response = requestTask.Result;
                               if (response.StatusCode == HttpStatusCode.OK)
                               {
                                   response.Content.ReadAsStringAsync().ContinueWith(
                                               (readTask) =>
                                               {
                                                   var result = readTask.Result.ToString();
                                                   JsonSerializerOptions options = new JsonSerializerOptions()
                                                   {
                                                       PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                                   };
                                                   var matismartRecloserStatus = JsonSerializer.Deserialize<MatismartRecloserStatusModel>(result, options);
                                                   if (matismartRecloserStatus != null)
                                                   {
                                                       item.Status = matismartRecloserStatus.IsOpened ? RecloserStatus.TurnedOn : RecloserStatus.TurnedOff;
                                                       foreach (var item in _matismartItemsRepository.items)
                                                       {
                                                           Console.Write(item.DivName + ":" + item.IsOpened.ToString() + " ");
                                                       }
                                                       Console.WriteLine(" ");

                                                   }
                                               });
                               }
                           });
                       Thread.Sleep(500);
                   }
                   Thread.Sleep(1000);
               }
           }
           );
        }

        public void OnPostTurnOnClicked(byte slaveId, string ipAddress, string port)
        {
            Console.WriteLine("Turn on " + slaveId.ToString());
        }

        public void OnPostTurnOffClicked(byte slaveId, string ipAddress, string port)
        {
            Console.WriteLine("Turn on " + slaveId.ToString());
        }
    }
}