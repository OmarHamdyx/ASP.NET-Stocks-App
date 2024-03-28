using Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;


namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly DefaultSymbol _defaultSymbol;

		public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration,IOptions<DefaultSymbol> defaultSymbol)
        {
            _defaultSymbol = defaultSymbol.Value;
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task<StockModel?> GetStockInfoAsync(string? symbol)
        {
			symbol ??= _defaultSymbol.Symbol;
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            { 
                HttpResponseMessage? httpResponseMessage =await httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");
                
                if (httpResponseMessage !=null)
                {
					string respnseAsString = await httpResponseMessage.Content.ReadAsStringAsync();
					StockModel? stockModel = JsonConvert.DeserializeObject<StockModel>(respnseAsString);
                    return stockModel;  
				}
				else 
                {
					throw new HttpRequestException($"Failed to retrieve stock details info for symbol {symbol}.");

				}
			}
		}
		public async Task<CompanyInfo?> GetCompanyInfoAsync(string? symbol)
		{
			symbol ??= _defaultSymbol.Symbol;

			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpResponseMessage? httpResponseMessage = await httpClient.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");

				if (httpResponseMessage != null)
				{
					string jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
					CompanyInfo? companyInfo = JsonConvert.DeserializeObject<CompanyInfo>(jsonString);
					return companyInfo;
				}
				else
				{
					throw new HttpRequestException($"Failed to retrieve company info for symbol {symbol}.");
				}
			}
		}

	}
}
