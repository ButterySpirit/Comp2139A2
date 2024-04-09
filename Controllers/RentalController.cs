using Assign1.Data;
using Assign1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


public class RentalController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Rental/Index
    public async Task<IActionResult> Index()
    {
        var rentals = await _context.Rentals.ToListAsync();
        return View(rentals);
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
        return View(rental);
    }

    // POST: Rental/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Rental rental)
    {
        if (id != rental.RentalId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rental);
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

    // GET: Rental/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rentals
            .FirstOrDefaultAsync(m => m.RentalId == id);
        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    // POST: Rental/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        _context.Rentals.Remove(rental);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RentalExists(int id)
    {
        return _context.Rentals.Any(e => e.RentalId == id);
    }
}
