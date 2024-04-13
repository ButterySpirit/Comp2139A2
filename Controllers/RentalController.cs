using Assign1.Data;
using Assign1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

[Route("Rental")]
public class RentalController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var rentals = await _context.Rentals.ToListAsync();
        return View(rentals);
    }

    [HttpGet("{id}")]
    public IActionResult Details(int id)
    {
        var rental = _context.Rentals.FirstOrDefault(r => r.RentalId == id);
        if (rental == null)
        {
            return NotFound();
        }
        return View(rental);
    }

    // GET: Rental/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Rental/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rental rental)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(rental);
    }

    // GET: Rental/Edit/5
    [HttpGet("Rental/Edit/{id:int}"), Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }
        ViewBag.AvailabilityOptions = new SelectList(new List<string> { "true", "false" });
        ViewBag.StatusOptions = new SelectList(new List<string> { "Available", "Rented", "Maintenance" });
        return View(rental);
    }

    // POST: Rental/Edit/
    [HttpPost("Rental/Edit/{id:int}"), ValidateAntiForgeryToken, Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Edit(int RentalId, [Bind("RentalId, VehicleName, VehicleType, State, Country, RentalCost, Availability, Status, LicensePlate, MakeModel, Description, PickupLocation, DropoffLocation")]
    Rental rental)
    {
        if (RentalId != rental.RentalId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                // Log or inspect the error message
                Console.WriteLine(error.ErrorMessage);
            }
        }

        if (ModelState.IsValid)
        {
            try
            {
                var rentalToUpdate = await _context.Rentals.FindAsync(RentalId);
                if (rentalToUpdate == null)
                {
                    return NotFound();
                }

                rentalToUpdate.VehicleName = rental.VehicleName;
                rentalToUpdate.VehicleType = rental.VehicleType;
                rentalToUpdate.State = rental.State;
                rentalToUpdate.Country = rental.Country;
                rentalToUpdate.RentalCost = rental.RentalCost;
                rentalToUpdate.Availability = rental.Availability;
                rentalToUpdate.Status = rental.Status;
                rentalToUpdate.LicensePlate = rental.LicensePlate;
                rentalToUpdate.MakeModel = rental.MakeModel;
                rentalToUpdate.Description = rental.Description;
                rentalToUpdate.PickupLocation = rental.PickupLocation;
                rentalToUpdate.DropoffLocation = rental.DropoffLocation;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(rental.RentalId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(rental);
    }

    // GET: Rental/Delete/
    [HttpGet("Rental/Delete/{id:int}"), Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rentals
            .FirstOrDefaultAsync(r => r.RentalId == id);
        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    // POST: Rental/Delete/5
    [HttpPost("Rental/Delete/{id:int}"), ActionName("DeleteConfirmed"), ValidateAntiForgeryToken, Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> DeleteConfirmed(int RentalId)
    {
        var rental = await _context.Rentals.FindAsync(RentalId);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return NotFound();
    }

    private bool RentalExists(int id)
    {
        return _context.Rentals.Any(e => e.RentalId == id);
    }
}
