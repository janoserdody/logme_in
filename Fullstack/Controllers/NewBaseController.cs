using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PassHomework.Models;
using PassHomework.Services.CurrencyExchange;


namespace PassHomework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewBaseController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private readonly IMemoryCache _cache;

        public NewBaseController(ICurrencyExchangeService currencyExchangeService, IMemoryCache memoryCache)
        {
            _currencyExchangeService = currencyExchangeService;
            _cache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewBase(string currency)
        {
            var exchangeRates = await LookupExchangeRate(currency);

            return exchangeRates == null
                ? (IActionResult)NotFound()
                : Ok(exchangeRates);
        }

        private async Task<ExchangeRates> LookupExchangeRate(string currency)
        {
            ExchangeRates exchangeRates;

            if (!_cache.TryGetValue(currency, out exchangeRates))
            {
                Console.WriteLine("Cache miss....loading from database into cache");

                exchangeRates = await _currencyExchangeService.GetNewBase(currency);

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60), // cache will expire in 60 seconds
                    SlidingExpiration = TimeSpan.FromSeconds(10) // caceh will expire if inactive for 10 seconds
                };

                _cache.Set(currency, exchangeRates);
            }
            else
            {
                Console.WriteLine("Cache hit");
            }

            return exchangeRates;
        }
    }
}
