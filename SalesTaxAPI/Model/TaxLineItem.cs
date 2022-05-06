using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace SalesTaxClientAPI.Model
{
    public class TaxLineItem
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("product_tax_code")]
        [JsonPropertyName("product_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonProperty("unit_price")]
        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("discount")]
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
    }
}


