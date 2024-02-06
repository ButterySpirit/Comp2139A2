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

    // POST: Hotel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("HotelId,HotelName,Address,City,PostalCode,State,Country,Status,StarRating,RoomType,NumberOfRooms,MaxOccupancy")] Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            var addHotel = new Hotel
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                Address = hotel.Address,
                City = hotel.City,
                PostalCode = hotel.PostalCode,
                State = hotel.State,
                Country = hotel.Country,
                CostPerNight = hotel.CostPerNight,
                Status = hotel.Status,
                StarRating = hotel.StarRating,
                RoomType = hotel.RoomType,
                NumberOfRooms = hotel.NumberOfRooms,
                MaxOccupancy = hotel.MaxOccupancy
            };

            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hotel);
    }

}

