using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Domain.Entities;
using Application.Interfaces;


namespace Application.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        private readonly DefaultSymbolOption _defaultSymbol;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IOptions<DefaultSymbolOption> defaultSymbol)
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
                HttpResponseMessage? httpResponseMessage = await httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");

                if (httpResponseMessage != null)
                {
                    string respnseAsString = await httpResponseMessage.Content.ReadAsStringAsync();

                    JObject responseObject = JObject.Parse(respnseAsString);

                    // Iterate over each property in the JObject
                    foreach (JProperty property in responseObject.Properties())
                    {
                        // Check if the value is null, if so, set it to zero
                        if (property.Value.Type == JTokenType.Null)
                        {
                            property.Value = JToken.FromObject(0);
                        }
                    }

                    // Deserialize the modified JObject back to a StockModel object
                    StockModel? stockModel = responseObject.ToObject<StockModel>();

                    return stockModel;
                }
                else
                {
                    throw new HttpRequestException($"Failed to retrieve stock details info for symbol {symbol}.");

                }
            }
        }
        public async Task<CompanyModel?> GetCompanyInfoAsync(string? symbol)
        {
            symbol ??= _defaultSymbol.Symbol;

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage? httpResponseMessage = await httpClient.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");

                if (httpResponseMessage != null)
                {
                    string jsonString = await httpResponseMessage.Content.ReadAsStringAsync();

                    CompanyModel? companyInfo = JsonConvert.DeserializeObject<CompanyModel>(jsonString);

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
