using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateTracking.Models
{
    public class ExchangeRate
    {
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }
        public IDictionary<string, IDictionary<string, decimal>> rates { get; set; }

    }

}
