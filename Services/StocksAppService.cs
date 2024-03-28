using Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly DefaultSymbol _defaultSymbol;

		public StocksAppService(IHttpClientFactory httpClientFactory, IConfiguration configuration,IOptions<DefaultSymbol> defaultSymbol)
        {
            _defaultSymbol = defaultSymbol.Value;
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<StockModel?> GetStockModelAsync(string? symbol)
        {
			symbol ??= _defaultSymbol.Symbol;
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            { 
                HttpResponseMessage? httpResponseMessage =await httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");
                string respnseAsString = await httpResponseMessage.Content.ReadAsStringAsync();    
                
				StockModel? stockModel = JsonConvert.DeserializeObject<StockModel>(respnseAsString);

				if (stockModel == null)
					throw new InvalidOperationException("No response from finnhub server");

				return stockModel;

			}
		}

        
    }
}
