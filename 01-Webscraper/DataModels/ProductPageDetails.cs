using Newtonsoft.Json;

namespace Webscraper.DataModels
{
    public class ProductPageDetails
    {
        public ProductPageDetails()
        {
            Title = null;
            Code = null;
            Energy = null;
            UnitPrice = 0.00d;
            Description = null;
            URL = null;
        }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("kcal_per_100g")]
        public double? Energy { get; set; }
        [JsonProperty("unit_price")]
        public double UnitPrice { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonIgnore]
        public string URL { get; set; }
    }
}