using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PassHomework.Models;
using PassHomework.Services.CurrencyExchange;

namespace PassHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private readonly IMemoryCache _cache;

        public ExchangeRatesController(ICurrencyExchangeService currencyExchangeService, IMemoryCache memoryCache)
        {
            _currencyExchangeService = currencyExchangeService;
            _cache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            var exchangeRates = await LookupExchangeRate();

            return exchangeRates == null
                ? (IActionResult) NotFound()
                : Ok(exchangeRates);
        }

        private async Task<ExchangeRates> LookupExchangeRate()
        {
            ExchangeRates exchangeRates;

            if (!_cache.TryGetValue("ExchangeRateUSD", out exchangeRates))
            {
                Console.WriteLine("Cache miss....loading from database into cache");

                exchangeRates = await _currencyExchangeService.GetLatestExchangeRates();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60), // cache will expire in 60 seconds
                    SlidingExpiration = TimeSpan.FromSeconds(10) // caceh will expire if inactive for 10 seconds
                };

                _cache.Set("ExchangeRateUSD", exchangeRates);
            }
            else
            {
                Console.WriteLine("Cache hit");
            }

            return exchangeRates;
        }
    }
}
