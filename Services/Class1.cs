using Interfaces;
using System.Net.Http;

namespace Services
{
    public class StocksAppService : IStocksAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StocksAppService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }
        


    }
}
