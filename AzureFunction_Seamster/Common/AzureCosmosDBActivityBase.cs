namespace AzureFunction_Seamster.Common
{
    internal class AzureCosmosDBActivityBase
    {

        public async Task<ProductFunctions.ItemResponse<Product>> SaveNewProductItem(Product objProduct)
        {
            Microsoft.Azure.Cosmos.ItemResponse<Product> ProductResponse = null;
            try
            {
                //  ProductResponse = await this.container.CreateItemAsync<Employee>(objProduct, new PartitionKey(objProduct.Name));
                employeeResponse = await this.container.CreateItemAsync<Product>(objProduct, new PartitionKey(objProduct.ProductId));
            }
            catch (CosmosException ex)
            {
                throw ex;
            }

        }
    }
}