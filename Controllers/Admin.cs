using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assign1.Data;
using Assign1.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assign1.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Flight management
        public async Task<IActionResult> AdminFlight()
        {
            try
            {
                var flights = await _context.Flights.ToListAsync();
                return View(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving flights.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // Add try-catch blocks and error handling for other CRUD actions...

        // Hotel management
        public async Task<IActionResult> AdminHotel()
        {
            try
            {
                var hotels = await _context.Hotels.ToListAsync();
                return View(hotels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving hotels.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // Rental management
        public async Task<IActionResult> AdminRental()
        {
            try
            {
                var rentals = await _context.Rentals.ToListAsync();
                return View(rentals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving rentals.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // Add try-catch blocks and error handling for other CRUD actions...

        // Common methods
        private bool EntityExists<T>(int id) where T : class
        {
            try
            {
                return _context.Set<T>().Find(id) != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if entity exists.");
                // Depending on how you use this method, you may want to handle the exception differently
                return false;
            }
        }
    }
}
