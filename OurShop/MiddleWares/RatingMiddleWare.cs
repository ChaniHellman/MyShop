using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services;
using System.Threading.Tasks;


namespace OurShop.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleWare
    {
        private readonly RequestDelegate _next;

        public RatingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating newRating = new Rating()
            {
                Host = httpContext.Request.Host.ToString(),
                Method = httpContext.Request.Method.ToString(),
                Path = httpContext.Request.Path.ToString(),
                Referer = httpContext.Request.Headers["Referer"].ToString(),
                UserAgent= httpContext.Request.Headers["User-Agent"].ToString(),
                RecordDate=DateTime.UtcNow

            };
            await ratingService.AddRating(newRating);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddleWareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleWare>();
        }
    }
}
