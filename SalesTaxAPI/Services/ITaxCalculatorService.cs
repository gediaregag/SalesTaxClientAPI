using System.Net.Http;
using System.Threading.Tasks;
using SalesTaxClientAPI.Model;

namespace SalesTaxClientAPI.Services
{
    public interface ITaxCalculatorService
    {
        Task<TaxResponse> CalculateTaxesForAnorder(Tax tax);
        Task<RateResponse> GetTaxRateForAlocation(Rate rate);
    }
}