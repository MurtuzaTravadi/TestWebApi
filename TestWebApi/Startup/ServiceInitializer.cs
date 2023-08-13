using Microsoft.AspNetCore.HttpsPolicy;
using TestWebApi.Middleware;
using TestWebApiBussiness;
using TestWebApiHelper;

namespace TestWebApi.Startup
{
    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {

            RegisterCustomDepencies(services);
            RegisterSwagger(services);
            return services;
        }

        private  static void RegisterCustomDepencies(IServiceCollection services)
        {
            services.AddTransient<IProduct, ProductLayer>();
            services.AddHttpClient<IHttpClientCall, HttpClientCall>();
            services.AddHttpClient(); // Register HttpClient as a singleton
            services.AddTransient<ExceptionMiddleware>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
