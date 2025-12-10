
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Web.API.Middlewares
{
    
    public class GlobalExcepcionHanldlingMiddlware : IMiddleware
    {
        private readonly ILogger<GlobalExcepcionHanldlingMiddlware> _logger;

        public GlobalExcepcionHanldlingMiddlware(ILogger<GlobalExcepcionHanldlingMiddlware> logger) => _logger = logger;


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                ProblemDetails details = new()
                {
                    Status=(int)HttpStatusCode.InternalServerError,
                    Type= "Server Error",
                    Title= "Server Error",
                    Detail= "An internal Server has ocurred",
                };

                string json=JsonSerializer.Serialize(details);
                context.Response.ContentType="application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
