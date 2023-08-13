using Microsoft.AspNetCore.Mvc;
using TestWebApiBussiness;
using TestWebApiModels;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProduct _product;
        public ProductsController(IProduct product)
        {
            _product = product;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ProdcutList> GetProducts()
        {
            return await _product.Get();
        }
    }
}
