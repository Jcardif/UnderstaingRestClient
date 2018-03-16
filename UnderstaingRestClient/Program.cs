using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnderstaingRestClient
{
    class Program
    {
        private static readonly HttpClient Client=new HttpClient();
        static void Main(string[] args)
        {
            ProcessRepositories().Wait();
        }

        private static async Task ProcessRepositories()
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var streamTask = await Client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            var repository = JsonConvert.DeserializeObject<List<Repo>>(streamTask);
            foreach (var repo in repository)
            {
                Console.WriteLine($"{repo.name}\t{repo.full_name}\t{repo.id}");
            }
        }
    }
}
