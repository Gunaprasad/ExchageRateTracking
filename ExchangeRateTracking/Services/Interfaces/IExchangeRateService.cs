using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateTracking.Services.Interfaces
{
    internal interface IExchangeRateService
    {
        Task<dynamic> GetExchangeRateHistory(DateTime startDate, DateTime endDate, string baseCurrency, string targetCurrency);
    }
}
