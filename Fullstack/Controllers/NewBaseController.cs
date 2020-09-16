using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassHomework.Services.CurrencyExchange;


namespace PassHomework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewBaseController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;
        public NewBaseController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewBase(string currency)
        {
            var exchangeRates = await _currencyExchangeService.GetNewBase(currency);

            return exchangeRates == null
                ? (IActionResult)NotFound()
                : Ok(exchangeRates);
        }
    }
}
