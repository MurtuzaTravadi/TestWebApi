using TestWebApiModels;

namespace TestWebApiBussiness
{
    public interface IProduct
    {
        public Task<ProdcutList> Get();
    }
}
