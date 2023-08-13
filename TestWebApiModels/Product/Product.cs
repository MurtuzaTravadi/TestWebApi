using Newtonsoft.Json;

namespace TestWebApiModels
{
    public class Product
    {
        public string? itemCode { get; set; }
        public string? name { get; set; }
        public string? manufacturer { get; set; }
        public string? mpn { get; set; }
        public decimal price { get; set; }
        public  string? upc { get; set; }
        public string? brand { get; set; }
        
    }

    //TODO we can utilize prodcut class as inheritace to reuse the property
    public class ProductXml
    {
        public string? itemCode { get; set; }
        public string? name { get; set; }
        public string? manufacturer { get; set; }
        public string? upc { get; set; }

        public string? mpn { get; set; }
        public string price { get; set; }
        public string? brand { get; set; }

    }

    public class ProductsResponse
    {
        [JsonProperty("@xmlns:xsi")]
        public string xmlnsxsi { get; set; }

        [JsonProperty("@xmlns:xsd")]
        public string xmlnsxsd { get; set; }
        public List<ProductXml> product { get; set; }
    }

    public class Root
    {
        [JsonProperty("?xml")]
        public Xml xml { get; set; }
        public ProductsResponse ProductsResponse { get; set; }
    }

    public class Xml
    {
        [JsonProperty("@version")]
        public string version { get; set; }

        [JsonProperty("@encoding")]
        public string encoding { get; set; }
    }
}
