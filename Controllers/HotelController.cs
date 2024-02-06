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
}

