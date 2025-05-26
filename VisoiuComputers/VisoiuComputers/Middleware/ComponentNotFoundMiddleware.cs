using VisoiuComputers.Core.Exceptions;

namespace VisoiuComputers.Api.Middleware
{
    public class ComponentNotFoundMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ComponentNotFoundMiddleware> _logger;

        public ComponentNotFoundMiddleware(RequestDelegate next, ILogger<ComponentNotFoundMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ComponentNotFoundException ex)
            {
                _logger.LogWarning(ex, "Component not found: {Message}", ex.Message);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                var response = new { error = ex.Message };
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        }
    }
}