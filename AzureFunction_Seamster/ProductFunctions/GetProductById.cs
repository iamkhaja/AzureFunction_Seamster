using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunction_Seamster.Models;

namespace AzureFunction_Seamster.ProductFunctions
{
    class GetProductById
    {
        private static object objProductResponse;

        [FunctionName("GetProductById")]
        public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetProductById/{ProductGUID}/{partitionkey}")] HttpRequest req,
           string productGUID, int partitionkey,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function - GetproductById.");

            ItemResponse<Product> objProductResponse = null;
            log.LogInformation("Calling Azure Function -- GetProductById");
            // initialising Azure Cosomosdb database connection.
            AzureCosmoDBActivity objCosmosDBActivitiy = new AzureCosmoDBActivity();
            await objCosmosDBActivitiy.InitiateConnection();
            // retriving existing employee information based on employee GUId and partition key i.e. employeeId value
            objProductResponse = (ItemResponse<Product>)await objCosmosDBActivitiy.GetProductItem(productGUID, partitionkey);

            return new OkObjectResult(objProductResponse.Resource);
        }
    }
}
