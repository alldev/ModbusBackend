using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModbusBackend.Repository;
using System.Net;
using System.Text.Json.Nodes;

namespace ModbusWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public readonly MatismartItemsRepository matismartItemsRepository;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            matismartItemsRepository = new();
        }

        public void OnGet()
        {
            Thread workerThread = new(() =>
            {
                while (true)
                {

                    /*

                    // Create an HttpClient instance 
                    HttpClient client = new();

                    // Send a request asynchronously continue when complete 
                    client.GetAsync("http://localhost/api/FirstFloor/getGarageAndEntranceLightStatus").ContinueWith(
                        (requestTask) =>
                        {
                            // Get HTTP response from completed task. 
                            HttpResponseMessage response = requestTask.Result;

                            // Check that response was successful or throw exception 
                            response.EnsureSuccessStatusCode();

                            // Read response asynchronously as JsonValue
                            response.Content.ReadAsStringAsync().ContinueWith(
                                        (readTask) =>
                                        {
                                            var result = readTask.Result.ToString();
                                            Console.WriteLine(result);
                                            //Do something with the result                   
                                        });
                        }); */

                    Thread.Sleep(1000);
                }
            });

            workerThread.Start();
        }
    }
}