using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assign1.Controllers
{
    public class SearchController : Controller
    {
        // GET: /<controller>/
        public IActionResult GeneralSearch(string destination, string tripType, DateTime? checkIn, DateTime? checkOut)
        {
            if (tripType == "Hotels")
            {
                return RedirectToAction("Search", "Hotel", new { destination });
            }

            if (tripType == "Rentals")
            {

            }

            return NotFound();
        }

        public IActionResult Flight(string departure, string arrival)
        {
            if (!String.IsNullOrEmpty(departure) && !String.IsNullOrEmpty(arrival))
            {
                // Redirect to the 'Search' action in the 'FlightController'
                return RedirectToAction("Search", "Flight", new { departure, arrival });
            }
            return NotFound();
        }



    }
}

