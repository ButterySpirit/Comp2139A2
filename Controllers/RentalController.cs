using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Assign1.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("Rental")]
public class RentalController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RentalController> _logger;

    public RentalController(ApplicationDbContext context, ILogger<RentalController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var rentals = await _context.Rentals.ToListAsync();
            return View(rentals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load rentals.");
            return RedirectToAction("Error500", "Home");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == id);
            if (rental == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to find rental details.");
            return RedirectToAction("Error500", "Home");
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rental rental)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create rental.");
                return RedirectToAction("Error500", "Home");
            }
        }
        return View(rental);
    }

    [HttpGet("Edit/{id:int}"), Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Error404", "Home");
        }

        try
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            ViewBag.AvailabilityOptions = new SelectList(new List<string> { "true", "false" });
            ViewBag.StatusOptions = new SelectList(new List<string> { "Available", "Rented", "Maintenance" });
            return View(rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load rental for editing.");
            return RedirectToAction("Error500", "Home");
        }
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Edit(int RentalId, [Bind("RentalId, VehicleName, VehicleType, State, Country, RentalCost, Availability, Status, LicensePlate, MakeModel, Description, PickupLocation, DropoffLocation")] Rental rental)
    {
        if (RentalId != rental.RentalId)
        {
            return RedirectToAction("Error404", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(rental);
        }

        try
        {
            _context.Update(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!RentalExists(rental.RentalId))
            {
                return RedirectToAction("Error404", "Home");
            }
            else
            {
                _logger.LogError(ex, "Concurrency issue on updating rental.");
                return RedirectToAction("Error500", "Home");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update rental.");
            return RedirectToAction("Error500", "Home");
        }
    }

    [HttpGet("Delete/{id:int}"), Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Error404", "Home");
        }

        try
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == id);
            if (rental == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to find rental for deletion.");
            return RedirectToAction("Error500", "Home");
        }
    }

    [HttpPost("Delete/{id:int}"), ActionName("DeleteConfirmed"), ValidateAntiForgeryToken, Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> DeleteConfirmed(int RentalId)
    {
        try
        {
            var rental = await _context.Rentals.FindAsync(RentalId);
            if (rental == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete rental.");
            return RedirectToAction("Error500", "Home");
        }
    }

    private bool RentalExists(int id)
    {
        return _context.Rentals.Any(e => e.RentalId == id);
    }
}
