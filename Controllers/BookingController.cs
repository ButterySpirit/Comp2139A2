using System;
using Microsoft.AspNetCore.Mvc;
using Assign1.Models;
using Assign1.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Assign1.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Create (Hotel)
        public IActionResult Create(int? id, decimal? costPerNight)
        {
            if (id.HasValue)
            {
                ViewBag.HotelId = id.Value;
            }
            if (costPerNight.HasValue)
            {
                ViewBag.CostPerNight = costPerNight.Value;
            }

            return View();
        }


        // POST: Booking/Create (Hotel)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TotalCost,BookingDate,PaymentStatus,ServiceType")] Booking booking, int id)
        {
            if (ModelState.IsValid)
            {
                booking.UserId = null; // Set UserId to null for guest users
                booking.ServiceType = "Hotel"; // Set ServiceType to "Hotel"
                ModelState.Remove("ServiceType"); // This will clear the error related to ServiceType
                booking.ServiceID = id; // Assign the HotelId

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingId });
            }
            return View(booking);
        }

        /*          Booking For Flights             */
        // GET: Booking/Create (Flight)
        [HttpGet("Booking/CreateFlight/{flightId}")]
        public IActionResult CreateFlight(int? flightId, decimal? ticketCost, string status)
        {
            if (flightId.HasValue)
            {
                ViewBag.FlightId = flightId;
            }
            if (ticketCost.HasValue)
            {
                ViewBag.TotalCost = ticketCost;
            }
            if (!String.IsNullOrEmpty(status))
            {
                ViewBag.Status = status;
            }
            return View();
        }


        // POST: Booking/Create (Flight)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFlight([Bind("TotalCost,BookingDate,PaymentStatus,ServiceType")] Booking booking, int flightId)
        {
            if (ModelState.IsValid)
            {
                booking.UserId = null; // Set UserId to null for guest users
                booking.ServiceType = "Flight"; // Set ServiceType to "Flight"
                ModelState.Remove("ServiceType"); // This will clear the error related to ServiceType
                booking.ServiceID = flightId; // Assign the Flight

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingId });
            }
            return View(booking);
        }

        //GET: Create Booking (Rental)
        [HttpGet("Booking/CreateRental/{rentalId}")]
        public IActionResult CreateRental(int? rentalId, decimal? rentalCost, string status)
        {
            if (rentalId.HasValue)
            {
                ViewBag.RentalId = rentalId;
            }
            if (rentalCost.HasValue)
            {
                ViewBag.TotalCost = rentalCost;
            }
            if (!String.IsNullOrEmpty(status))
            {
                ViewBag.Status = status;
            }
            return View();
        }

        //POST: Create Booking (Rental)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRental([Bind("TotalCost,BookingDate,PaymentStatus,ServiceType")] Booking booking, int rentalId)
        {
            if (ModelState.IsValid)
            {
                booking.UserId = null; // Set UserId to null for guest users
                booking.ServiceType = "Rental"; // Set ServiceType to "Rental"
                ModelState.Remove("ServiceType"); // This will clear the error related to ServiceType
                booking.ServiceID = rentalId; // Assign the Rental

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingId });
            }
            return View(booking);
        }

        // GET: Booking/Confirmation/{id} (Hotel)
        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
    }
}