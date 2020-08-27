using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PassHomework.Models;

namespace PassHomework.Services.CurrencyExchange
{
    /// <summary>
    /// Uses https://exchangeratesapi.io/
    /// </summary>
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CurrencyExchangeService> _logger;

        private const string LatestExchangeRatesEndpoint = "https://api.exchangeratesapi.io/latest";
        private static readonly JsonSerializerOptions CamelCaseOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public CurrencyExchangeService(IHttpClientFactory httpClientFactory, ILogger<CurrencyExchangeService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ExchangeRates> GetLatestExchangeRates()
        {
            using var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(LatestExchangeRatesEndpoint);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error calling '{LatestExchangeRatesEndpoint}'. StatusCode: {response.StatusCode}");

                return null;
            }
            var responseStream = await response.Content.ReadAsStreamAsync();

            try
            {
                return await JsonSerializer.DeserializeAsync<ExchangeRates>(responseStream, CamelCaseOptions);
            }
            catch (JsonException exception)
            {
                _logger.LogError(exception, $"Error deserializing JSON response of '{LatestExchangeRatesEndpoint}'");

                return null;
            }
        }
    }
}
