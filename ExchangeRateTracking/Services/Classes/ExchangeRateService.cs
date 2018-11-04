using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExchangeRateTracking.Models;
using ExchangeRateTracking.Services.Interfaces;
using Newtonsoft.Json;

namespace ExchangeRateTracking.Services.Classes
{
    public class ExchangeRateService : IExchangeRateService
    {
        private const string _baseurl = @"https://api.exchangeratesapi.io/";
        private decimal maxcurrencyvalue;
        private DateTime mincurrencydate;
        private decimal mincurrencyvalue;
        private decimal averagevalue;
        private DateTime maxcurrencydate;

        public async Task<dynamic> GetExchangeRateHistory(DateTime startDate, DateTime endDate, string baseCurrency, string targetCurrency)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource using HttpClient  
                var response = await client.GetAsync("history?start_at=" + startDate.Date.ToString("yyyy-MM-dd") + "&end_at=" + endDate.Date.ToString("yyyy-MM-dd") + "&base=" + baseCurrency);
                //Checking the response is successful or not which is sent using HttpClient  
                if (!response.IsSuccessStatusCode) return null;
                //Storing the response details recieved from web api
                var content = response.Content.ReadAsStringAsync().Result;
                //Deserizling Json Result in to a Model
                var lstexchangerate = JsonConvert.DeserializeObject<ExchangeRate>(content);

                //Required linq operation to select the target currency exchange rate
                var lsttargetrate = lstexchangerate.rates.Select(x => new { x.Key, value = x.Value[targetCurrency] });
                var maxtargetrate = lsttargetrate.OrderByDescending(x => x.value).FirstOrDefault();
                var mintargetrate = lsttargetrate.OrderBy(x => x.value).FirstOrDefault();
                
                var lstresult = new List<string>();
                lstresult.Add("A min rate of " + mintargetrate.value.ToString() + " on " + mintargetrate.Key + " ");
                lstresult.Add("A max rate of " + maxtargetrate.value.ToString() + " on " + maxtargetrate.Key + " ");
                lstresult.Add("An average rate of " + lsttargetrate.Average(x=>x.value).ToString());
            
                return JsonConvert.SerializeObject(lstresult);
            }
        }
    }
}
