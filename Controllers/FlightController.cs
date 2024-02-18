using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assign1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assign1.Controllers
{
    public class FlightController : Controller
    {

        private readonly ApplicationDbContext _context;

        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var flight = _context.Flights.ToList();
            return View(flight);
        }

        public IActionResult Details(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }
    }
}

