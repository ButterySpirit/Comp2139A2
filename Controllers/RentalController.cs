using Assign1.Data;
using Assign1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetching available rentals directly using the Rental model
            var availableRentals = await _context.Rentals
                .Where(r => r.Availability) // Assumes there's an 'Availability' property in your Rental model
                .ToListAsync();

            return View(availableRentals);
        }
    }
}