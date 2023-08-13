namespace TestWebApiHelper
{
    public interface IHttpClientCall
    {
        public Task<string> GetCall(string url, Dictionary<string, string> keyValuePairs);
    }
}
