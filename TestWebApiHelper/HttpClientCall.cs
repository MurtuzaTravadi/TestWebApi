using Microsoft.Extensions.Configuration;
using TestWebApi;

namespace TestWebApiHelper
{
    public class HttpClientCall : IHttpClientCall
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string? AccessToken { get; set; }

        public HttpClientCall(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
        }

        public async Task<string> GetCall(string url, Dictionary<string, string> keyValuePairs)
        {
            await SetAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                // Handle non-successful HTTP responses here
                // For example: throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            if (string.IsNullOrWhiteSpace(AccessToken))
            {
                AccessToken = await OAuthHelper.GetAccessTokenAsync(_configuration).ConfigureAwait(false);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            }
        }
    }
}



