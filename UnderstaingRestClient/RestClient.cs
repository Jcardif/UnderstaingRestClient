using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
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
        private static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static void ShowProductList(List<Product> productlist)
        {
            Console.Clear();
            foreach (var product in productlist)
            {
                Console.WriteLine($"Id : {product.Id}\nName : {product.Name}\nPrice : {product.Price}\n{product.Category}\n\n");
            }
        }

        static void ShowProduct(Product product)
        {
            Console.Clear();
            Console.WriteLine($"Id : {product.Id}\nName : {product.Name}\nPrice : {product.Price}\n{product.Category}");
        }

        static async Task RunAsync()
        {
            Client.BaseAddress=new Uri("http://localhost:60154");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));

            try
            {
                Console.WriteLine("1. Get All\n2. Get With Id\n3. Create an Object");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Enter the Id of the product");
                        ShowProduct(await GetProductAsync(Convert.ToInt32(Console.ReadLine())));
                        Console.ReadKey();
                        Main();
                        break;
                    case 2:
                        ShowProductList(await GetProductListAsync());
                        Console.ReadKey();
                        Main();
                        break;
                    case 3:
                        Product prdt = new Product()
                        {
                            Name = "Casio690",
                            Price = 2300,
                            Category = "Calculators"
                        };
                        //Console.Clear();
                        Console.WriteLine($"{prdt.Name} has been created at {await CreateProductAsync(prdt)}");
                        Main();
                        break;
                    default:
                        Console.WriteLine("Choose a value between 1 and 4");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static async Task<List<Product>> GetProductListAsync()
        {
            List<Product>lstproduct = new List<Product>();
            HttpResponseMessage response = await Client.GetAsync("api/Product");
            if (response.IsSuccessStatusCode)
            {
                lstproduct = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            }
            return lstproduct;
        }

        private static async Task<Product> GetProductAsync(int id)
        {
            Product product=new Product();
            HttpResponseMessage response = await Client.GetAsync($"api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        private static async Task<Uri> CreateProductAsync(Product product)
        {
            Product prdt = new Product()
            {
                Name = "Casio690",
                Price = 2300,
                Category = "Calculators"
            };
            HttpResponseMessage response = await Client.PostAsync("api/Product", prdt,new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
    }
}
