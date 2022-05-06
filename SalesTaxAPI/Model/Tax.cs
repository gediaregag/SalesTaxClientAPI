using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace SalesTaxClientAPI.Model
{
    public class Tax
    {
        [JsonProperty("from_country")]
        [JsonPropertyName("from_country")]
        public string FromCountry { get; set; }

        [JsonProperty("from_zip")]
        [JsonPropertyName("from_zip")]
        public string FromZip { get; set; }

        [JsonProperty("from_state")]
        [JsonPropertyName("from_state")]
        public string FromState { get; set; }

        [JsonProperty("from_city")]
        [JsonPropertyName("from_city")]
        public string FromCity { get; set; }

        [JsonProperty("from_street")]
        [JsonPropertyName("from_street")]
        public string FromStreet { get; set; }

        [JsonProperty("to_country")]
        [JsonPropertyName("to_country")]
        [Required]
        public string ToCountry { get; set; }

        [JsonProperty("to_zip")]
        [JsonPropertyName("to_zip")]
        public string ToZip { get; set; }

        [JsonProperty("to_state")]
        [JsonPropertyName("to_state")]
        public string ToState { get; set; }

        [JsonProperty("to_city")]
        [JsonPropertyName("to_city")]
        public string ToCity { get; set; }

        [JsonProperty("to_street")]
        [JsonPropertyName("to_street")]
        public string ToStreet { get; set; }

        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("shipping", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("shipping")]
        [Required]
        public decimal Shipping { get; set; }

        [JsonProperty("customer_id")]
        [JsonPropertyName("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("exemption_type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonProperty("nexus_addresses")]
        [JsonPropertyName("nexus_addresses")]
        public List<NexusAddress> NexusAddresses { get; set; }

        [JsonProperty("line_items")]
        [JsonPropertyName("line_items")]
        public List<TaxLineItem> LineItems { get; set; }
    }
}


