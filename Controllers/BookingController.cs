using System;
using Microsoft.AspNetCore.Mvc;
using Assign1.Models;
using Assign1.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace Assign1.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case where the user ID isn't found
                return View(new List<Booking>()); // Return an empty list or redirect
            }

            var bookings = await _context.Bookings
                                         .Where(b => b.UserId == userId)
                                         .ToListAsync();
            return View(bookings);
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookingId, TotalCost, BookingDate, PaymentStatus, ServiceType, ServiceID")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: Booking/Create (Hotel)
        [Authorize]
        [HttpGet("Booking/Create/{hotelId}")]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TotalCost,BookingDate,PaymentStatus,ServiceType")] Booking booking, int id)
        {
            // Remove any ModelState errors for UserId
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                // Assign UserId from the logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                booking.UserId = userId;
                Debug.WriteLine("UserId: " + booking.UserId);

                booking.ServiceType = "Hotel"; // Set ServiceType to "Hotel"
                ModelState.Remove("ServiceType"); // This will clear the error related to ServiceType

                booking.ServiceID = id; // Assign the HotelId

                _context.Add(booking);
                await _context.SaveChangesAsync();

                var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
                var viewModel = new BookingViewModel
                {
                    Booking = booking,
                    Hotel = hotel
                };

                // Return a JSON response for AJAX request
                return PartialView("_HotelConfirmed", viewModel);
            }

            return View(booking);
        }

        /*          Booking For Flights             */
        // GET: Booking/Create (Flight)
        [Authorize]
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


        // HTTP POST CreateFlight
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFlight([Bind("TotalCost,BookingDate,PaymentStatus,ServiceType")] Booking booking, int flightId)
        {
            // Remove any ModelState errors for UserId
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                // Assign UserId from the logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                booking.UserId = userId;
                Debug.WriteLine("UserId: " + booking.UserId);

                // Other booking properties
                booking.ServiceType = "Flight";
                booking.ServiceID = flightId;

                // Add to the context and save changes
                _context.Add(booking);
                await _context.SaveChangesAsync();

                // Generate a view model for the confirmation view
                var viewModel = new BookingViewModel
                {
                    Booking = booking,
                    Flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId)
                };

                return PartialView("_FlightConfirmed", viewModel);
            }
            else
            {
                // Debugging: Log ModelState errors
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Debug.WriteLine("Error: " + error.ErrorMessage);
                    }
                }
            }

            return View(booking);
        }


        //GET: Create Booking (Rental)
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRental([Bind("TotalCost,BookingDate,PaymentStatus,ServiceType")] Booking booking, int rentalId)
        {
            // Remove any ModelState errors for UserId
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                // Assign UserId from the logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                booking.UserId = userId;
                Debug.WriteLine("UserId: " + booking.UserId);

                booking.ServiceType = "Rental"; // Set ServiceType to "Rental"
                ModelState.Remove("ServiceType"); // This will clear the error related to ServiceType

                booking.ServiceID = rentalId; // Assign the Rental

                var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == rentalId);
                var viewModel = new BookingViewModel
                {
                    Booking = booking,
                    Rental = rental
                };

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return PartialView("_RentalConfirmed", viewModel);
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