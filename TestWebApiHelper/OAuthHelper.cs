using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TestWebApiModels.Token;

namespace TestWebApi
{
    public static class OAuthHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> GetAccessTokenAsync(IConfiguration configuration)
        {
            var tokenEndpoint = configuration["OAuth:TokenEndpoint"];

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", configuration["OAuth:ClientId"] },
                { "client_secret", configuration["OAuth:ClientSecret"] },
                { "scope", "products" }
            });

            var response = await _httpClient.PostAsync(tokenEndpoint, content);
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Token>(responseContent).access_token;
        }
    }
}