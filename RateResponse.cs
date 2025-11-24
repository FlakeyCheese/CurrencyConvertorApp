using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CurrencyConvertorApp
{
    //  Model for the 'latest' endpoint (to get rates relative to EUR)
    public class RateResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        // This dictionary holds the exchange rates (e.g., "USD": 1.09)
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
