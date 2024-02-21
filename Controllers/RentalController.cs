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

        // Search for rentals
        public IActionResult Search(string vehicleName, string vehicleType, string state)
        {
            var rentalQuery = _context.Rentals.AsQueryable();

            if (!String.IsNullOrEmpty(vehicleName))
            {
                rentalQuery = rentalQuery.Where(r => r.VehicleName.ToLower().Contains(vehicleName.ToLower()));
            }

            if (!String.IsNullOrEmpty(vehicleType))
            {
                rentalQuery = rentalQuery.Where(r => r.VehicleType == vehicleType);
            }

            if (!String.IsNullOrEmpty(state))
            {
                rentalQuery = rentalQuery.Where(r => r.State == state);
            }

            var rentals = rentalQuery.ToList();
            return View("Index", rentals);
        }
    }
}