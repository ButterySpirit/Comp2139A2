using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Assign1.Services.Filters
{
    public class LogUserActivityFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LogUserActivityFilter(ILogger<LogUserActivityFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Log before the action executes
            _logger.LogInformation("Starting action {action} on controller {controller}",
                context.ActionDescriptor.RouteValues["action"],
                context.ActionDescriptor.RouteValues["controller"]);

            // Example: Log details of search queries
            if (context.ActionArguments.ContainsKey("searchQuery"))
            {
                _logger.LogInformation("Search query: {query}", context.ActionArguments["searchQuery"]);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Log after the action has executed
            _logger.LogInformation("Completed action {action} on controller {controller}",
                context.ActionDescriptor.RouteValues["action"],
                context.ActionDescriptor.RouteValues["controller"]);
        }
    }
}
