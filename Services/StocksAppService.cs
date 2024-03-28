using Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Services
{
    public class StocksAppService : IStocksAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

		public StocksAppService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<StockModel?> GetStockModelAsync(string? symbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            { 
                HttpResponseMessage? httpResponseMessage =await httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");
                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();    
                StreamReader reader = new StreamReader(stream);
                string responseMessage=reader.ReadToEnd();
				StockModel? stock = JsonConvert.DeserializeObject<StockModel>(responseMessage);
                return stock;
                
			}
		}

        
    }
}
