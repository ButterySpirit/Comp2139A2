using System.Linq;
using Assign1.Data;
using Assign1.Models;
using Microsoft.AspNetCore.Mvc;

public class HotelController : Controller
{
    private readonly ApplicationDbContext _context;

    public HotelController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Hotel/Create
    public IActionResult Index()
    {
        var hotel = _context.Hotels.ToList();
        return View(hotel);
    }

    public IActionResult Details(int id)
    {
        var hotel = _context.Hotels.FirstOrDefault(p => p.HotelId == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }

    /*
    public IActionResult Search(string destination, int rating, DateTime? checkIn, DateTime? checkOut)
    {
        var hotelQuery = _context.Hotels.AsQueryable();

        if (!String.IsNullOrEmpty(destination))
        {
            hotelQuery = hotelQuery.Where(h => h.State.Contains(destination));
        }

        if (rating > 0)
        {
            hotelQuery = hotelQuery.Where(h => h.StarRating >= rating);
        }

        if (checkIn.HasValue)
        {
            hotelQuery = hotelQuery.Where(h => h.StartDate <= checkIn.Value);
        }

        if (checkOut.HasValue)
        {
            hotelQuery = hotelQuery.Where(h => h.EndDate >= checkOut.Value);
        }

        var hotels = hotelQuery.ToList();
        ViewData["SearchPerformed"] = !String.IsNullOrEmpty(destination) || checkIn.HasValue || checkOut.HasValue;
        ViewData["SearchString"] = destination;

        return View("Index", hotels);
    }
    */

}

