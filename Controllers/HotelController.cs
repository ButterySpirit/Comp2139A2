using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Assign1.Models;
using Microsoft.Extensions.Logging;

[Route("Hotel")]
public class HotelController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HotelController> _logger;

    public HotelController(ApplicationDbContext context, ILogger<HotelController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Hotel/Index
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var hotels = await _context.Hotels.ToListAsync();
            return View(hotels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load hotels.");
            return RedirectToAction("Error500", "Home");
        }
    }

    // GET: Hotel/Details/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to find hotel details.");
            return RedirectToAction("Error500", "Home");
        }
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
            try
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create hotel.");
                return RedirectToAction("Error500", "Home");
            }
        }
        return View(hotel);
    }

    // GET: Hotel/Edit/5
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
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve hotel for edit.");
            return RedirectToAction("Error500", "Home");
        }
    }

    // POST: Hotel/Edit/
    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Edit(int id, [Bind("HotelId, HotelName, Description, Address, City, PostalCode, State, Country, CostPerNight, StartDate, EndDate, Status, StarRating, RoomType, NumberOfRooms, MaxOccupancy, PhoneNumber, CancellationPolicy, CheckInTime, CheckOutTime, image")] Hotel hotel)
    {
        if (id != hotel.HotelId)
        {
            return RedirectToAction("Error404", "Home");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!HotelExists(hotel.HotelId))
                {
                    return RedirectToAction("Error404", "Home");
                }
                else
                {
                    _logger.LogError(ex, "Concurrency issue on updating hotel.");
                    return RedirectToAction("Error500", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update hotel.");
                return RedirectToAction("Error500", "Home");
            }
        }
        return View(hotel);
    }

    // GET: Hotel/Delete/
    [HttpGet("Delete/{id:int}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to find hotel for deletion.");
            return RedirectToAction("Error500", "Home");
        }
    }

    // POST: Hotel/Delete/
    [HttpPost("Delete/{id:int}"), ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> DeleteConfirmed(int HotelId)
    {
        try
        {
            var hotel = await _context.Hotels.FindAsync(HotelId);
            if (hotel == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete hotel.");
            return RedirectToAction("Error500", "Home");
        }
    }

    private bool HotelExists(int id)
    {
        return _context.Hotels.Any(e => e.HotelId == id);
    }
}
