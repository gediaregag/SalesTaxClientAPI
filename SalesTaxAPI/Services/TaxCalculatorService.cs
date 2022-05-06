using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesTaxClientAPI.Model;
using SalesTaxClientAPI.Infrastructure;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace SalesTaxClientAPI.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private HttpClient _httpContent { get; }
        private IConfiguration _Configuration { get; }

        public TaxCalculatorService(HttpClient httpContent, IConfiguration configuration)
        {
            _Configuration = configuration;
            httpContent.BaseAddress = new Uri(_Configuration.GetSection("API:Url").Value);
            httpContent.DefaultRequestHeaders.Accept.Clear();
            httpContent.DefaultRequestHeaders.Add("Authorization", $"Bearer {_Configuration.GetSection("API:Key").Value}");
            httpContent.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContent = httpContent;
        }

        public async Task<RateResponse> GetTaxRateForAlocation(Rate rate)
        {
            string endpoint = $"{_Configuration.GetSection("API:Version").Value}{_Configuration.GetSection("API:TaxRatelocationEndPoint").Value}";
            string parameters = $"?zip={rate.Zip}&country={rate.Country}&state={rate.State}&city={rate.City}&street={rate.Street}";
            HttpResponseMessage response = await _httpContent.GetAsync($"{endpoint}{parameters}").ConfigureAwait(false);
            string returnMessage = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode >= 400)
            {
                TaxError taxError = JsonConvert.DeserializeObject<TaxError>(returnMessage);
                string errorMessage = taxError.Error + " - " + taxError.Detail;
                throw new TaxException(response.StatusCode, taxError, errorMessage);
            }

            return JsonConvert.DeserializeObject<RateResponse>(returnMessage); ;
        }



        public async Task<TaxResponse> CalculateTaxesForAnorder(Tax tax)
        {
            string endpoint = $"{_Configuration.GetSection("API:Version").Value}{_Configuration.GetSection("API:CalculateTaxesForAnorderEndPoint").Value}";
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(tax), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpContent.PostAsync(endpoint, requestContent).ConfigureAwait(false);
            string returnMessage = await response.Content.ReadAsStringAsync();

            if ((int)response.StatusCode >= 400)
            {
                TaxError taxError = JsonConvert.DeserializeObject<TaxError>(returnMessage);
                string errorMessage = taxError.Error + " - " + taxError.Detail;
                throw new TaxException(response.StatusCode, taxError, errorMessage);
            }

            return JsonConvert.DeserializeObject<TaxResponse>(returnMessage); ;
        }
    }
}
