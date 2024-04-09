using System.Linq;
using Assign1.Data;
using Assign1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


public class HotelController : Controller
{
    private readonly ApplicationDbContext _context;

    public HotelController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Hotel/Index
    public IActionResult Index()
    {
        var hotels = _context.Hotels.ToList();
        return View(hotels);
    }

    // GET: Hotel/Details/5
    public IActionResult Details(int id)
    {
        var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    // GET: Hotel/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Hotel/Create
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("HotelName,Address,City,State,Country,StarRating,Description,CostPerNight,image")] Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hotel);
    }

    // GET: Hotel/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    // POST: Hotel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("HotelId,HotelName,Address,City,State,Country,PostalCode,StarRating,Description,CostPerNight,image")] Hotel hotel)
    {
        if (id != hotel.HotelId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hotel);
    }

    // GET: Hotel/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hotel = await _context.Hotels
            .FirstOrDefaultAsync(h => h.HotelId == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return View(hotel);
    }

    // POST: Hotel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);
        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
