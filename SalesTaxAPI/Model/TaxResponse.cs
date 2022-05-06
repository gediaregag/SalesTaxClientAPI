using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace SalesTaxClientAPI.Model
{
    public class TaxResponse
    {
        [JsonPropertyName("tax")]
        public TaxResponseAttributes Tax { get; set; }
    }

    public class TaxResponseAttributes
    {
        [JsonPropertyName("order_total_amount")]
        public decimal OrderTotalAmount { get; set; }

        [JsonPropertyName("shipping")]
        public decimal Shipping { get; set; }

        [JsonPropertyName("taxable_amount")]
        public decimal TaxableAmount { get; set; }

        [JsonPropertyName("amount_to_collect")]
        public decimal AmountToCollect { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonPropertyName("tax_source")]
        public string TaxSource { get; set; }

        [JsonPropertyName("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonPropertyName("jurisdictions")]
        public TaxJurisdictions Jurisdictions { get; set; }

        [JsonPropertyName("breakdown")]
        public TaxBreakdown Breakdown { get; set; }
    }
}


