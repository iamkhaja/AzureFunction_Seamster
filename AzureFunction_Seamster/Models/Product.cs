using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction_Seamster.Models
{
    class Product
    {

        [JsonProperty("ProductGuid")]
        public string ProductGuid { get; set; }

        [JsonProperty("ProductId")]
        public string ProductId { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("OrderName")]
        public string OrderName { get; set; }

        [JsonProperty("OrderId")]
        public string OrderId { get; set; }

        [JsonProperty("OrderType")]
        public string OrderType { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("ProductType")]
        public string ProductType { get; set; }

        [JsonProperty("Productid")]
        public string Productid { get; set; }

        public class Employees
        {
            public Employees()
            {
                LstEmployee = new List<Product>();
            }
            public List<Product> LstEmployee { get; set; }
        }
    }

}

