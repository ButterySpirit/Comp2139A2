using System;
namespace Assign1.Services.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request
            _logger.LogInformation("Handling request: {Method} {Url}", context.Request.Method, context.Request.Path);

            // Call the next delegate/middleware in the pipeline
            await _next(context);

            // Log the outgoing response
            _logger.LogInformation("Handled response: {StatusCode}", context.Response?.StatusCode);
        }
    }

}

