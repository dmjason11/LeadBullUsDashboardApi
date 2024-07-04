using Api.Errors;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _host;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _host = hostEnvironment;
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "Application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _host.IsDevelopment() ? new ApiResponseDetails((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                var opt = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(response, opt);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
