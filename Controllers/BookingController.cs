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

        // GET: Booking/Create
        public IActionResult Create(int? hotelId, decimal? costPerNight)
        {
            if (hotelId.HasValue)
            {
                ViewBag.HotelId = hotelId.Value;
            }
            if (costPerNight.HasValue)
            {
                ViewBag.CostPerNight = costPerNight.Value;
            }

            return View();
        }


        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,TotalCost,BookingDate,PaymentStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingId });
            }
            return View(booking);
        }

        // GET: Booking/Confirmation/{id}
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