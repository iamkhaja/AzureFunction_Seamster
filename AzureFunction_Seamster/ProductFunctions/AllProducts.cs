using AzureFunction_Seamster.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction_Seamster.ProductFunctions
{
    class AllProducts
    {
        private static object lstProducts;

        [FunctionName("AllProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AllProducts")] HttpRequest req,
            ILogger log)
        {
            //declaring the response
            List<Product> lstProducts = null;
            log.LogInformation("Calling Azure Function -- ProductGetId");
            // initialising Azure Cosomosdb database connection.
            AzureCosmoDBActivity objCosmosDBActivitiy = new AzureCosmoDBActivity();
            await objCosmosDBActivitiy.InitiateConnection();
            // retriving existing Product information based on ProductGuid and partition key i.e. ProductId value
            lstProducts = await objCosmosDBActivitiy.GetAllProducts(); ;
            return new OkObjectResult(lstProducts);
        }
    }

    internal class AzureCosmoDBActivity
    {
        public AzureCosmoDBActivity()
        {
        }

        internal Task<List<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        internal Task<object> GetProductItem(string productGUID, int partitionkey)
        {
            throw new NotImplementedException();
        }

        internal Task InitiateConnection()
        {
            throw new NotImplementedException();
        }
    }
}
