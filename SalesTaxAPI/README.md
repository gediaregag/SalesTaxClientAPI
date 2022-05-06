# Sales Tax Client API

### How to run on local server
Build and run the API from Visual Studio, then swagger UI will lauch by default.
<p>Default port is 5000.</p>

### Environment
netcoreapp 3.1

## Endpoints
### Show tax rates for a location [`GET`]
>Shows the sales tax rates for a given location.<br>
> [http://localhost:5000/api/TaxCalculator/GetTaxRateForAlocation](http://localhost:5000/api/TaxCalculator/GetTaxRateForAlocation).<br/>
> [https://localhost:5001/api/TaxCalculator/GetTaxRateForAlocation](https://localhost:5001/api/TaxCalculator/GetTaxRateForAlocation). * secure.
``` csharp
### Note on query Parameters
[`country`] -string -conditional -Two-letter ISO country code for given location. For international locations outside of US, `country` is required.
 [`zip`]  -string -required -Postal code for given location(5-Digit ZIP or ZIP+4).
[`state`] -string -optional -Two-letter ISO state code for given location.
 [`city`] -string -optional -City for given location.
 [`street`] -string -optional -Street address for given location.
 ```
 >##### Example request
 http://localhost:5000/api/TaxCalculator/GetTaxRateForAlocation?Zip=22315
 ``` json
 Example Response 
 {
  "rate": {
    "zip": "22315",
    "state": "VA",
    "state_rate": 0.043,
    "county": "FAIRFAX",
    "county_rate": 0.01,
    "city": "FRANCONIA",
    "city_rate": 0,
    "combined_district_rate": 0.007,
    "combined_rate": 0.06,
    "freight_taxable": false,
    "country": "US",
    "name": null,
    "country_rate": 0,
    "standard_rate": 0,
    "reduced_rate": 0,
    "super_reduced_rate": 0,
    "parking_rate": 0,
    "distance_sale_threshold": 0
  }
}
 
 ```
 
### Calculate sales tax for an order[`POST`]

> Shows the sales tax for a given order.<br/>
> [http://localhost:5000/api/TaxCalculator/CalculateTaxesForAnorder](http://localhost:5000/api/TaxCalculator/CalculateTaxesForAnorder).<br/>
> [https://localhost:5001/api/TaxCalculator/CalculateTaxesForAnorder](https://localhost:5001/api/TaxCalculator/CalculateTaxesForAnorder). * secure. 

``` csharp
 ### Note on Request body
  [`to_country`] -string -required	-Two-letter ISO country code of the country where the order shipped to.
 [`to_zip`] - string -conditional -Postal code where the order shipped to (5-Digit ZIP or ZIP+4). If `to_country` is 'US', `to_zip` is required
  [`to_state`] -string -conditional -Two-letter ISO state code where the order shipped to. If `to_country` is 'US' or 'CA', `to_state` is required.
[`shipping`] -float -required -Total amount of shipping for the order.
[`nexus_addresses[][country]`]  -string -conditional -Two-letter ISO country code for the nexus address. If providing `nexus_addresses`, country is required.
  [`nexus_addresses[][state]`]  -string -conditional -Two-letter ISO state code for the nexus address. If providing `nexus_addresses`, state is required.
  ....` other parameters are optional.

 ```
```json
Example request 

{
  "from_country": "US",
  "from_zip":"92093",
  "from_state": "CA",
  "from_city":"La Jolla",
  "from_street":"9500 Gilman Drive",
  "to_country" : "US",
  "to_zip" : "90002",
  "to_state" :  "CA",
  "to_city" :  "Los Angeles",
  "to_street": "1335 E 103rd St",
  "amount": 15,
  "shipping": 1.5,
  "nexus_addresses":[{
      "id" :"Main Location",
      "country":"US",
      "zip": "92093",
      "state" : "CA",
      "city" : "La Jolla",
      "street" :"9500 Gilman Drive"
    }],
  "line_items":[ {
      "id": "1",
      "quantity": 1,
      "product_tax_code":"20010",
      "unit_price" : 15,
      "discount" : 0
    }]
  }

Example Response 

{
  "tax": {
    "orderTotalAmount": 0,
    "shipping": 1.5,
    "taxableAmount": 0,
    "amountToCollect": 0,
    "rate": 0.095,
    "hasNexus": false,
    "freightTaxable": false,
    "taxSource": null,
    "exemptionType": null,
    "jurisdictions": {
      "country": "US",
      "state": "CA",
      "county": "LOS ANGELES COUNTY",
      "city": "LOS ANGELES"
    },
    "breakdown": {
      "state_tax_rate": 0.0625,
      "state_tax_collectable": 0.94,
      "county_tax_collectable": 0.15,
      "city_tax_collectable": 0,
      "special_district_taxable_amount": 15,
      "special_tax_rate": 0.0225,
      "special_district_tax_collectable": 0.34,
      "taxable_amount": 15,
      "tax_collectable": 1.43,
      "combined_tax_rate": 0.095,
      "state_taxable_amount": 15,
      "county_taxable_amount": 15,
      "county_tax_rate": 0.01,
      "city_taxable_amount": 0,
      "city_tax_rate": 0,
      "country_taxable_amount": 0,
      "country_tax_rate": 0,
      "country_tax_collectable": 0,
      "gst_taxable_amount": 0,
      "gst_tax_rate": 0,
      "gst": 0,
      "pst_taxable_amount": 0,
      "pst_tax_rate": 0,
      "pst": 0,
      "qst_taxable_amount": 0,
      "qst_tax_rate": 0,
      "qst": 0
    }
  }
```
