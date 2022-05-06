using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace SalesTaxClientAPI.Model
{
    public class Rate
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        [Required]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }

}
