namespace AzureFunction_Seamster.Common
{
    internal class QueryDefinition
    {
        private string sqlQueryText;

        public QueryDefinition(string sqlQueryText)
        {
            this.sqlQueryText = sqlQueryText;
        }
    }
}