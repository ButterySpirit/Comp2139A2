using System;
using System.Linq;
using System.Threading.Tasks;
using Assign1.Data;
using Assign1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assign1.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Flight")]
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FlightController> _logger;

        public FlightController(ApplicationDbContext context, ILogger<FlightController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Flight
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var flights = await _context.Flights.ToListAsync();
                return View(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flights list.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Flight/Details/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == id);
                if (flight == null)
                {
                    return RedirectToAction("Error404", "Home");
                }
                return View(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flight details.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Flight/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flight/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,AirlineName,DeparturePort,ArrivalPort,AvailSeats,TicketCost,StartDate,EndDate,Status,FlightDuration")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(flight);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating flight.");
                    return RedirectToAction("Error500", "Home");
                }
            }
            return View(flight);
        }

        // GET: Flight/Edit/
        [HttpGet("Edit/{id:int}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            try
            {
                var flight = await _context.Flights.FindAsync(id);
                if (flight == null)
                {
                    return RedirectToAction("Error404", "Home");
                }
                return View(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flight for edit.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Flight/Edit/
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,AirlineName,DeparturePort,ArrivalPort,AvailSeats,TicketCost,StartDate,EndDate,Status,FlightDuration")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return RedirectToAction("Error404", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!FlightExists(flight.FlightId))
                    {
                        return RedirectToAction("Error404", "Home");
                    }
                    _logger.LogError(ex, "Concurrency error occurred while editing flight.");
                    return RedirectToAction("Error500", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating flight.");
                    return RedirectToAction("Error500", "Home");
                }
            }
            return View(flight);
        }

        // GET: Flight/Delete/
        [HttpGet("Delete/{id:int}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == id);
                if (flight == null)
                {
                    return RedirectToAction("Error404", "Home");
                }
                return View(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flight for deletion.");
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Flight/Delete/
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int FlightId)
        {
            try
            {
                var flight = await _context.Flights.FindAsync(FlightId);
                if (flight == null)
                {
                    return RedirectToAction("Error404", "Home");
                }
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flight.");
                return RedirectToAction("Error500", "Home");
            }
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }
    }
}
