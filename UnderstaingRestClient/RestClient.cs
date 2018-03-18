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
        private  static readonly HttpClient Client=new HttpClient();
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

        private static async Task RunAsync()
        {
            Client.BaseAddress=new Uri("http://localhost:60154");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/Json"));

            try
            {
                Console.WriteLine("1. Get All\n2. Get With Id\n3. Create an Object\n4. Modify Existing Product");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.Clear();
                        ShowProductList(await GetProductListAsync());
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Enter the Id of the product");
                        ShowProduct(await GetProductAsync(Convert.ToInt32(Console.ReadLine())));
                        Console.ReadKey();
                        break;
                    case 3:
                        Product prdt = new Product();
                        Console.Write("Name : ");
                        prdt.Name = Console.ReadLine();
                        Console.Write("Category : ");
                        prdt.Category = Console.ReadLine();
                        Console.Write("Price : ");
                        prdt.Price = Convert.ToDouble(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine($"{prdt.Name} has been created at {await CreateProductAsync(prdt)}");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Enter the Id of the product to modify");
                        var id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Product Details");
                        Product prdt1 = new Product();
                        Console.Write("Name : ");
                        prdt1.Name = Console.ReadLine();
                        Console.Write("Category : ");
                        prdt1.Category = Console.ReadLine();
                        Console.Write("Price : ");
                        prdt1.Price = Convert.ToDouble(Console.ReadLine());
                        await UpdateProductAsync(id, prdt1);
                        Console.Clear();
                        Console.WriteLine("Product updated successfully as below");
                        ShowProduct(await GetProductAsync(id));
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
            finally
            {
                
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
            var response = await Client.PostAsJsonAsync("api/Product", product);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

        private static async Task UpdateProductAsync(int id, Product product)
        {
            var response = await Client.PutAsJsonAsync($"api/product/{id}", product);
            response.EnsureSuccessStatusCode();
        }
    }
}
