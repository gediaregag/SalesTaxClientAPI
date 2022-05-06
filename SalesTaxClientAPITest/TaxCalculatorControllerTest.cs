using System;
using Xunit;
using Moq;
using SalesTaxClientAPI.Services;
using SalesTaxClientAPI.Controllers;
using SalesTaxClientAPI.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SalesTaxClientAPITest
{
    public class TaxCalculatorControllerTest
    {
        private readonly Mock<ITaxCalculatorService> _mock;

        public TaxCalculatorControllerTest()
        {
            _mock = new Mock<ITaxCalculatorService>();
        }

        #region GetTaxRateForAlocation Tests
        // mock data
        private RateResponse GetRateResponse() => new RateResponse
        {
            Rate = new RateResponseAttributes
            {
                Zip = "22315",
                State = "VA",
                StateRate = 0.043M,
                County = "FAIRFAX",
                CountyRate = 0.01M,
                City = "FRANCONIA",
                CityRate = 0,
                CombinedDistrictRate = 0.007M,
                CombinedRate = 0.06M,
                FreightTaxable = false,
                Country = "US",
                Name = null,
                CountryRate = 0,
                StandardRate = 0,
                ReducedRate = 0,
                SuperReducedRate = 0,
                ParkingRate = 0,
                DistanceSaleThreshold = 0
            }
        };

        [Fact]
        public async Task GetTaxRateForAlocation_Valid_And_OkResult()
        {
            //Arrange
            var rateResponse = GetRateResponse();
            var rate = new Rate { Zip = "22315" };
            _mock.Setup(x => x.GetTaxRateForAlocation(rate))
                     .ReturnsAsync(rateResponse);

            var _controller = new TaxCalculatorController(_mock.Object);

            //Act
            var actionResult = await _controller.GetTaxRateForAlocation(rate);
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(rateResponse.Equals(result.Value));

        }

        [Fact]
        public async Task GetTaxRateForAlocation_NotFound()
        {

            //Arrange
            RateResponse rateResponse = new RateResponse { Rate = null };
            Rate rate = new Rate { Zip = null, Country = null, City = null, State = null, Street = null };
            _mock.Setup(x => x.GetTaxRateForAlocation(rate))
                     .ReturnsAsync(rateResponse);

            var _controller = new TaxCalculatorController(_mock.Object);

            //Act
            var actionResult = await _controller.GetTaxRateForAlocation(rate);
            var result = actionResult.Result as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }


        [Fact]
        public async Task GetTaxRateForAlocation_500InternalServerError()
        {

            //Arrange
            _mock.Setup(x => x.GetTaxRateForAlocation(It.IsAny<Rate>()))
                     .Throws(new Exception());

            var _controller = new TaxCalculatorController(_mock.Object);

            //Act
            var actionResult = await _controller.GetTaxRateForAlocation(null);
            var _500StatusCode = ((ObjectResult)actionResult.Result).StatusCode;


            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, _500StatusCode);

        }

        #endregion

        #region CalculateTaxesForAnorder Tests

        private Tax GetTaxInput() => new Tax
        {
            ToCountry = "US",
            ToZip = "90002",
            ToState = "CA",
            ToCity = "Los Angeles",
            Shipping = 1.5M
        };


        private TaxResponse GetTaxResponse() => new TaxResponse
        {
            Tax = new TaxResponseAttributes
            {
                OrderTotalAmount = 0,
                Shipping = 1.5M,
                TaxableAmount = 0,
                Rate = 0,
                HasNexus = false,
                FreightTaxable = false,
                TaxSource = null,
                ExemptionType = null,
                Jurisdictions = null,
                Breakdown = null,
            }
        };

        [Fact]
        public async Task CalculateTaxesForAnorder_Valid_And_OkResult()
        {
            //Arrange
            var taxInput = GetTaxInput();
            var taxResponse = GetTaxResponse();
            _mock.Setup(x => x.CalculateTaxesForAnorder(taxInput))
                    .ReturnsAsync(taxResponse);

            var _controller = new TaxCalculatorController(_mock.Object);

            //Act
            var actionResult = await _controller.CalculateTaxesForAnorder(taxInput);
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(taxResponse.Equals(result.Value));

        }

        [Fact]
        public async Task CalculateTaxesForAnorder_NotFound()
        {

            //Arrange
            TaxResponse taxResponse = new TaxResponse { Tax = null };
            Tax taxInput = new Tax {ToCountry = null,ToZip = null, ToState = null,ToCity = null, Shipping = -1 };
            _mock.Setup(x => x.CalculateTaxesForAnorder(taxInput))
                     .ReturnsAsync(taxResponse);

            var _controller = new TaxCalculatorController(_mock.Object);

            //Act
            var actionResult = await _controller.CalculateTaxesForAnorder(taxInput);
            var result = actionResult.Result as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CalculateTaxesForAnorder_500InternalServerError()
        {

            //Arrange
            _mock.Setup(x => x.CalculateTaxesForAnorder(It.IsAny<Tax>()))
                     .Throws(new Exception());

            var _controller = new TaxCalculatorController(_mock.Object);

            //Act
            var actionResult = await _controller.CalculateTaxesForAnorder(null);
            var _500StatusCode = ((ObjectResult)actionResult.Result).StatusCode;


            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, _500StatusCode);

        }
        #endregion

    }
}
