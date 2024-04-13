using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Assign1.Models;
using Microsoft.Extensions.Logging;

namespace Assign1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Custom Error handling action
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        var statusCode = HttpContext.Response.StatusCode;
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };

        if (exceptionFeature != null)
        {
            // Log the path where the exception occurred along with the exception message
            _logger.LogError("An error occurred on path: {Path} with exception: {Exception}",
                exceptionFeature.Path, exceptionFeature.Error);

            errorViewModel.ExceptionMessage = exceptionFeature.Error.Message;
            errorViewModel.Path = exceptionFeature.Path;
        }

        // Return different views based on the status code
        if (statusCode == 404)
        {
            return View("Error404", errorViewModel); // Ensure you have an Error404.cshtml view in Views/Shared or Views/Home
        }

        return View("Error", errorViewModel); // General error view for all other types of errors
    }
}
