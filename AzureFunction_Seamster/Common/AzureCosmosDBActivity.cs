using AzureFunction_Seamster.Models;
using AzureFunction_Seamster.ProductFunctions;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AzureFunction_Seamster.Common
{
    class AzureCosmosDBActivity : AzureCosmosDBActivityBase
    {

        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = "https://localhost:8081";
        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "MyLearning";
        // private string containerId = "ProductContainer";
        private string containerId = "ProductLatest";
        private List<Product> lstProduct;
        private object employeeResponse;

        public List<Product> ProductId { get; private set; }
        public object ProductGuid { get; private set; }

        public async Task InitiateConnection()
        {
            // Create a new instance of the Cosmos Client 
            //configuring Azure Cosmosdb sql api details
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            await CreateDatabaseAsync();
            await CreateContainerAsync();
        }

        private async Task CreateDatabaseAsync()
        {
            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }


        /// <summary>
        /// Create the container if it does not exist. 
        /// Specifiy "/ProductName" as the partition key since we're storing Product information, to ensure good distribution of requests and storage.
        /// </summary>
        /// <returns></returns>
        private async Task CreateContainerAsync()
        {
            // Create a new container
            //this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Product");
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/ProductId");
        }

        public async Task<ProductFunctions.ItemResponse<Product>> ModifyEmployeeItem(Product objProduct)
        {
            ProductFunctions.ItemResponse<Product> ProductResponse = null;
            try
            {
                /* Note : Partition Key value should not change */
                // EmployeeResponse = await this.container.ReplaceItemAsync<Employee>(objEmployee, objEmployee.EmployeeId, new PartitionKey(objEmployee.Name));
                ProductResponse = await this.container.ReplaceItemAsync<Product>(objProduct, objProduct.ProductId, new PartitionKey(objProduct.ProductId));
            }
            catch (CosmosException ex)
            {
                throw ex;
            }
            return ProductResponse;
        }


        public async Task<ProductFunctions.ItemResponse<Product>> GetProductItem(string ProductId, int partionKey)
        {
            ProductFunctions.ItemResponse<Product> ProductResponse = null;
            try
            {
                ProductResponse = await this.container.ReadItemAsync<Product>(ProductId, new PartitionKey(partionKey));
            }
            catch (CosmosException ex)
            {
                throw ex;
            }
            return ProductResponse;
        }

        public async Task<List<Product>> GetProducts()
        {
            var sqlQueryText = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Product> queryResultSetIterator = this.container.GetItemQueryIterator<Product>(queryDefinition);

            List<Product> lstProducts = new List<Product>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Product> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                lstProducts = currentResultSet.Select(r => new Product()
                {
                    ProductId = r.ProductId,
                    ProductName = r.ProductName,
                    ProductType = r.ProductType,

                }).ToList();

            }
            return lstProducts;
        }

        private class ProductResponse
        {
        }
    }

         internal class AzureCosmosDBActivityBase
  
    
    
    {
             }

    

        public class Database
        {
            internal Task<Container> CreateContainerIfNotExistsAsync(string containerId, string v)
            {
                throw new NotImplementedException();
            }
        }

        public class CosmosClient
        {
            private string endpointUri;
            private string primaryKey;

            public CosmosClient(string endpointUri, string primaryKey)
            {
                this.endpointUri = endpointUri;
                this.primaryKey = primaryKey;
            }

            internal Task<Database> CreateDatabaseIfNotExistsAsync(string databaseId)
            {
                throw new NotImplementedException();
            }
        }

        [Serializable]
        public class CosmosException : Exception
        {
            public CosmosException()
            {
            }

            public CosmosException(string message) : base(message)
            {
            }

            public CosmosException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected CosmosException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }

    internal class Container
    {
        internal Task<object> CreateItemAsync<T>(T objProduct, PartitionKey partitionKey)
        {
            throw new NotImplementedException();
        }

        internal FeedIterator<Product> GetItemQueryIterator<T>(QueryDefinition queryDefinition)
        {
            throw new NotImplementedException();
        }

        internal Task<AzureFunction_Seamster.ProductFunctions.ItemResponse<T>> ReadItemAsync<T>(string productId, PartitionKey partitionKey)
        {
            throw new NotImplementedException();
        }

        internal Task<AzureFunction_Seamster.ProductFunctions.ItemResponse<T>> ReplaceItemAsync<T>(T objProduct, string productsGuid, PartitionKey partitionKey)
        {
            throw new NotImplementedException();
        }

    public static implicit operator Container(ContainerResponse v)
    {
        throw new NotImplementedException();
    }
}

