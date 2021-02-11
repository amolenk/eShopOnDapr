using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class BasketClient
    {
        private readonly HttpClient httpClient;

        public BasketClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetItems() =>
            await httpClient.GetFromJsonAsync<string>("basketId");
    }
}
