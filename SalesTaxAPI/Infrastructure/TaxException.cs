using System;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace SalesTaxClientAPI.Infrastructure
{
    [Serializable]
    public class TaxException : ApplicationException
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public TaxError TaxjarError { get; set; }

        public TaxException(HttpStatusCode statusCode, TaxError taxError, string message) : base(message)
        {
            HttpStatusCode = statusCode;
            TaxjarError = taxError;
        }
    }

    public class TaxError
    {
        [JsonProperty("error")]
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonProperty("detail")]
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public string StatusCode { get; set; }
    }
}
