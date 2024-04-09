using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assign1.Data;
using Assign1.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Assign1.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Flight management
        public async Task<IActionResult> AdminFlight()
        {
            var flights = await _context.Flights.ToListAsync();
            return View(flights);
        }

        // Add other CRUD actions for Flight here...

        // Hotel management
        public async Task<IActionResult> AdminHotel()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return View(hotels);
        }

        // Add other CRUD actions for Hotel here...

        // Rental management
        public async Task<IActionResult> AdminRental()
        {
            var rentals = await _context.Rentals.ToListAsync();
            return View(rentals);
        }

        // Add other CRUD actions for Rental here...

        // Common methods
        private bool EntityExists<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id) != null;
        }
    }
}
