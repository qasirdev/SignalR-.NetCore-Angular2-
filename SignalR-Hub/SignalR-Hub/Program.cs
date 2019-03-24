using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SignalRHub
{
    public class Program
    {
        //https://medium.com/@rukshandangalla/how-to-notify-your-angular-5-app-using-signalr-5e5aea2030b2

        static HttpClient client = new HttpClient();
        public static void Main(string[] args)
        {            
            BuildWebHost(args).Run();
            RunAsync().GetAwaiter().GetResult();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static async Task<Uri> PostAsync(Message message)
        {
            string stringData = JsonConvert.SerializeObject(message);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                "api/Message/Post", contentData);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }


        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:1874/api/Message/Post");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Message message = new Message
                {
                    Type = "success",
	                Payload = "This is our success message"
                };

                var url = await PostAsync(message);
                Console.WriteLine($"Created at {url}");

                //// Get the product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                //// Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

    }
}
