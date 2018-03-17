using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestService.Models;

namespace UnderstandingRestClient
{
    internal class RestClient
    {
        private static readonly HttpClient Client=new HttpClient();
        private static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static void ShowProduct(List<Product> productlist)
        {
            Console.WriteLine("Product Name\tPrice\tCategory");
            foreach (var product in productlist)
            {
                Console.WriteLine($"{product.Name}\t{product.Price}\t{product.Category}");
            }
        }

        static async Task RunAsync()
        {
            Client.BaseAddress=new Uri("http://localhost:60154");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));
            try
            {
               var product = await GetProductAsync();
                ShowProduct(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task<List<Product>> GetProductAsync()
        {
            List<Product>lstproduct = new List<Product>();
            HttpResponseMessage response = await Client.GetAsync("api/Product");
            if (response.IsSuccessStatusCode)
            {
                lstproduct = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            }
            return lstproduct;
        }
    }
}
