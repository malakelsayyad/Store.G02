using Domain.Exceptions;
using Shared.ErrorModels;

namespace Store.G02.Api.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;

        public GlobalErrorHandlingMiddleWare(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HandlingNotFoundEnpointAsync(context);
            }
            catch (Exception ex)
            {
                //Log Exception
                _logger.LogError(ex, ex.Message);
                await HandlingErrorAsync(context, ex);
            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            //1.Set Status Code For Response
            //2.Set Content Type Code For Response
            //3.Response Object (Body)
            //4.Return Response

            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEnpointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"Endpoint {context.Request.Path} Is Not Found"
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
