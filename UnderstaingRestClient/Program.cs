using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

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
            var streamTask = Client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var serilizer = new DataContractJsonSerializer(typeof(List<Repo>));
            Console.WriteLine(message);
        }
    }
}
