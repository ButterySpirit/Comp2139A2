using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assign1.Data;
using Assign1.Models;
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

        public IActionResult Search(string departure, string destination)
        {
            var flightQuery = _context.Flights.AsQueryable();

            if (!String.IsNullOrEmpty(departure))
            {
                flightQuery = flightQuery.Where(f => f.DeparturePort == departure);
            }

            if (!String.IsNullOrEmpty(destination))
            {
                flightQuery = flightQuery.Where(f => f.ArrivalPort == destination);
            }

            var flights = flightQuery.ToList();
            return View("Index", flights);
        }

    }
}

