using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TestWebApiHelper;
using TestWebApiModels;

namespace TestWebApiBussiness
{
    public class ProductLayer : IProduct
    {
        private readonly IHttpClientCall _httpClientCall;
        private readonly IConfiguration _configuration;

        public ProductLayer(IHttpClientCall httpClientCall, IConfiguration configuration)
        {
            _httpClientCall = httpClientCall ?? throw new ArgumentNullException(nameof(httpClientCall));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<ProdcutList> Get()
        {
            return await GetMatchingProductAsync();
        }

        private async Task<ProdcutList> GetMatchingProductAsync()
        {
            var bannerProductsTask = GetDataFromBannerAsync();
            var rsHughesProductsTask = GetDataFromRSHughesAsync();

            await Task.WhenAll(bannerProductsTask, rsHughesProductsTask);

            var bannerProducts = bannerProductsTask.Result;
            var rsHughesProducts = rsHughesProductsTask.Result;

            var matchingProducts = bannerProducts.products
                .Where(bannerProduct => rsHughesProducts.products.Any(rsProduct => rsProduct.upc == bannerProduct.upc))
                .ToList();

            return new ProdcutList { products = matchingProducts };
        }

        private async Task<ProdcutList> GetDataFromBannerAsync()
        {
            string result = await _httpClientCall.GetCall(_configuration["DkhdevJson"], new Dictionary<string, string>());
            return JsonSerializer.Deserialize<ProdcutList>(result);
        }

        private async Task<ProdcutList> GetDataFromRSHughesAsync()
        {
            string result = await _httpClientCall.GetCall(_configuration["DkhdevXML"], new Dictionary<string, string>());
            var xmlProduct = JsonSerializer.Deserialize<Root>(GlobalFuntion.ConvertXMLtoJson(result))?.ProductsResponse?.product;
            var jsonProducts = ConvertXmlProductToJson(xmlProduct);
            return new ProdcutList { products = jsonProducts };
        }

        private List<Product>? ConvertXmlProductToJson(List<ProductXml> products)
        {
            return products?.Select(productXml => new Product
            {
                brand = productXml.brand,
                itemCode = productXml.itemCode,
                manufacturer = productXml.manufacturer,
                mpn = productXml.mpn,
                upc = productXml.upc,
                name = productXml.name,
                price = decimal.TryParse(productXml.price, out var parsedPrice) ? parsedPrice : 0
            }).ToList();
        }
    }
}