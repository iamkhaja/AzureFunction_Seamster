using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
//using AzureFunctionV3Learning.Models;
//using AzureFunctionV3Learning.Common;
//using Microsoft.Azure.Cosmos;
using AzureFunction_Seamster.Models;
using System.Collections.Generic;
using System.Linq;

namespace AzureFunction_Seamster.ProductFunctions
{
    public class ProductInsert
    {
        private static object objProductDetails;

        [FunctionName("ProductInsert")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ProductInsert")] HttpRequest req,
            ILogger log)
        {
            // we are initialised required variables 
            string requestBody = null;
            Product objProductDetails = null;
            MyAzureFunctionResponse objResponse = new MyAzureFunctionResponse();
            ItemResponse<Product> objInsertResponse = null;
            log.LogInformation("Calling Azure Function -- ProductInsert");

            // we are reading or parsing the input request body
            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            objProductDetails = JsonConvert.DeserializeObject<Product>(requestBody);
            ItemResponse<List<Product>> objProductResponse = null;
            if (objProductDetails != null)
            {
                // initialising Azure Cosomosdb database connection.
                AzureCosmoDBActivity objCosmosDBActivitiy = new AzureCosmoDBActivity();
                await objCosmosDBActivitiy.InitiateConnection();

                // saving new student information.
                // System need to generate one unique value for "id" property while saving new items in container
                objProductDetails.ProductId = Guid.NewGuid().ToString();
                var obj = await objCosmosDBActivitiy.GetAllProducts();
                var a = obj.OrderByDescending(p => p.OrderId).Select(o => o.OrderId);
                Product insertResponse = await objCosmosDBActivitiy.(objProductDetails);
                if (objInsertResponse == null)
                {
                    objResponse.ErrorCode = 100;
                    objResponse.Message = $"Error occured while inserting information of s- {insertResponse.ProductName}, {insertResponse.Productid}";
                    log.LogInformation(objResponse.Message + "Error:" + objInsertResponse.StatusCode);
                }
                else
                {
                    objResponse.ErrorCode = 0;
                    objResponse.Message = "Successfully inserted information.";
                }

            }
            else
            {
                objResponse.ErrorCode = 100;
                objResponse.Message = "Failed to read or extract Product information from Request body due to bad data.";
                log.LogInformation("Failed to read or extract Product information from Request body due to bad data.");
            }
            return new OkObjectResult(objResponse);
        }
    }

    internal class ItemResponse<T>
    {
        public string StatusCode { get; internal set; }
        public object Resource { get; internal set; }
    }

    internal class MyAzureFunctionResponse
    {
        public MyAzureFunctionResponse()
        {
        }

        public int ErrorCode { get; internal set; }
        public string Message { get; internal set; }
    }
}
