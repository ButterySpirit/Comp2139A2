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

    // POST: Hotel/Edit/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("HotelId, HotelName, Description, Address, City, PostalCode, State, Country, CostPerNight, StartDate, EndDate, Status, StarRating, RoomType, NumberOfRooms, MaxOccupancy, PhoneNumber, CancellationPolicy, CheckInTime, CheckOutTime, image")] Hotel hotel)
    {
        if (id != hotel.HotelId)
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
                _context.Update(hotel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(hotel.HotelId))
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
        return View(hotel);
    }


    // GET: Hotel/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    // POST: Hotel/Delete/
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int HotelId)
    {
        var hotel = await _context.Hotels.FindAsync(HotelId);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return NotFound();
    }

    private bool HotelExists(int id)
    {
        return _context.Hotels.Any(e => e.HotelId == id);
    }
}
