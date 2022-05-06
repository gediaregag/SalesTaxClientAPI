using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesTaxClientAPI.Infrastructure;
using SalesTaxClientAPI.Model;
using SalesTaxClientAPI.Services;

namespace SalesTaxClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    //[DisableCors]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculatorService _ItaxCalculator;

        public TaxCalculatorController(ITaxCalculatorService ItaxCalculator)
        {
            _ItaxCalculator = ItaxCalculator;
        }
        /// <summary>
        /// Shows the sales tax rates for a given location.
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        /// <remarks>
        /// <br>Parameters</br>
        /// <list type="bullet">
        /// <item><description><br>`country` -string -conditional -Two-letter ISO country code for given location. For international locations outside of US, `country` is required.</br></description></item>
        /// <item><description><br>`zip`  -string -required -Postal code for given location(5-Digit ZIP or ZIP+4).</br></description></item>
        /// <item><description><br>`state` -string -optional -Two-letter ISO state code for given location.</br></description></item>
        /// <item><description><br>`city` -string -optional -City for given location.</br></description></item>
        ///<item><description><br>`street` -string -optional -Street address for given location.</br></description></item>
        ///</list>
        /// </remarks>
        //GET: api/GetTaxRateForAlocation
        [HttpGet]
        [Route("GetTaxRateForAlocation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RateResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[DisableCors]
        public async Task<ActionResult<RateResponse>> GetTaxRateForAlocation([FromQuery] Rate rate)
        {
            try
            {
                var response = await _ItaxCalculator.GetTaxRateForAlocation(rate);

                if(response.Rate == null || response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ExceptionFormat.BasicInformation(ex));
            }
        }


        /// <summary>
        /// Shows the sales tax that should be collected for a given order.
        /// </summary>
        /// <param name="tax"></param>
        /// <returns></returns>
        /// <remarks>
        /// <br>Request body</br>
        /// <list type="bullet">
        /// <item><description><br>`to_country` -string -required	-Two-letter ISO country code of the country where the order shipped to.</br></description></item>
        /// <item><description><br>`to_zip` -string -conditional -Postal code where the order shipped to (5-Digit ZIP or ZIP+4). If `to_country` is 'US', `to_zip` is required</br></description></item>
        /// <item><description><br>`to_state` -string -conditional -Two-letter ISO state code where the order shipped to. If `to_country` is 'US' or 'CA', `to_state` is required.</br></description></item>
        /// <item><description><br>`shipping` -float -required -Total amount of shipping for the order.</br></description></item>
        ///<item><description><br>`nexus_addresses[][country]` -string -conditional -Two-letter ISO country code for the nexus address. If providing `nexus_addresses`, country is required.</br></description></item>
        ///<item><description><br>`nexus_addresses[][state]` -string -conditional -Two-letter ISO state code for the nexus address. If providing `nexus_addresses`, state is required.</br></description></item>
        ///<item><description><br>`....` other parameters are optional.</br></description></item>
        ///</list>
        /// </remarks>
        // POST api/CalculateTaxesForAnorder
        [HttpPost]
        [Route("CalculateTaxesForAnorder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaxResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaxResponse>> CalculateTaxesForAnorder([FromBody] Tax tax)
        {
            try
            {
                var response = await _ItaxCalculator.CalculateTaxesForAnorder(tax);

                if (response.Tax == null || response == null)
                {
                    return NotFound();
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ExceptionFormat.BasicInformation(ex));
            }
        }

    }
}
