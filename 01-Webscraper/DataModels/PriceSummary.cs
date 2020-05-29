using Newtonsoft.Json;

namespace Webscraper.DataModels
{
    public class PriceSummary
    {
        [JsonProperty("net")]
        public double Net { get; set; }
        [JsonProperty("vat")]
        public double VAT { get; set; }
        [JsonProperty("gross")]
        public double Gross { get; set; }
    }
}
