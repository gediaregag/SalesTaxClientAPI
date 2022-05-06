using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace SalesTaxClientAPI.Model
{
    public class NexusAddress
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        [JsonPropertyName("street")]
        public string Street { get; set; }
    }
}
