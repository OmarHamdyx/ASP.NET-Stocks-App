using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;
using Interfaces;

namespace Services
{
	public class CompanyNameService : ICompanyNameService
	{
		private readonly DefaultSymbol _defaultSymbol;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public CompanyNameService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IOptions<DefaultSymbol> defaultSymbol)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
			_defaultSymbol = defaultSymbol.Value;
		}

		public async Task<CompanyInfo?> GetCompanyInfoAsync(string? symbol)
		{
			symbol ??= _defaultSymbol.Symbol;

			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpResponseMessage? response = await httpClient.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={_configuration["FinnhubApiKey"]}");

				if (response.IsSuccessStatusCode)
				{
					string jsonString = await response.Content.ReadAsStringAsync();
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
