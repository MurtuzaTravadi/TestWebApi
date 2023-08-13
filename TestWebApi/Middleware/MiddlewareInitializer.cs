using TestWebApi.Middleware;

namespace TestWebApi
{
    public static partial class MiddlewareInitializer
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
          => app.UseMiddleware<ExceptionMiddleware>();
    }
}
