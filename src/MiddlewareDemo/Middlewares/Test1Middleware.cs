using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo.Middlewares
{
    #region Test1Middleware

    public class Test1Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Add("x-CustomHeader1", "CustomHeaderContent");
            await next(context);
        }
    }

    public static class MiddlewareTest1Extensions
    {
        public static IApplicationBuilder UseTest1Middleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Test1Middleware>();
        }
    }
    
    #endregion
}
