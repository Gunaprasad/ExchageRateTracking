using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExchangeRateTracking.Models;
using ExchangeRateTracking.Services.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ExchangeRateTracking.Controllers
{
    [Route("api/[controller]")]
    public class ExchangeRateController : Controller
    {
        private const string _baseurl = @"https://api.exchangeratesapi.io/";

        [HttpGet("GetExchangeRateHistory/{startDate}/{endDate}/{baseCurrency}/{targetCurrency}")]
        public async Task<ActionResult> GetExchangeRateHistory(DateTime startDate, DateTime endDate, string baseCurrency, string targetCurrency)

        {

            var _service = new ExchangeRateService();

            var res = await _service.GetExchangeRateHistory(startDate, endDate, baseCurrency, targetCurrency);
 
            return Ok(res);
        }
    }
}