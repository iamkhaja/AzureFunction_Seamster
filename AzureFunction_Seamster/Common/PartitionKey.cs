namespace AzureFunction_Seamster.Common
{
    internal class PartitionKey
    {
        private int productId;

        public PartitionKey(int productId)
        {
            this.productId = productId;
        }
    }
}