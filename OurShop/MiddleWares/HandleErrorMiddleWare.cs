using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OurShop.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HandleErrorMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestDelegate> _logger;

        public HandleErrorMiddleWare(RequestDelegate next, ILogger<RequestDelegate> logger)
        {

            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occured while processing the request");
                httpContext.Response.StatusCode= StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("An unexpected error...");
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HandleErrorMiddleWareExtensions
    {
        public static IApplicationBuilder UseHandleErrorMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandleErrorMiddleWare>();
        }
    }
}
