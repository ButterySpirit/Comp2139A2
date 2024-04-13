using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Microsoft.Extensions.Logging;

namespace Assign1.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ApplicationDbContext context, ILogger<SearchController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> HotelSearch(string destination, int rating, DateTime? checkIn, DateTime? checkOut)
        {
            try
            {
                if (!string.IsNullOrEmpty(destination))
                {
                    var hotelQuery = _context.Hotels.AsQueryable();

                    if (!string.IsNullOrEmpty(destination))
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

                    var hotels = await hotelQuery.ToListAsync();
                    return PartialView("_HotelResults", hotels);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for hotels.");
                // Optionally pass an error message or model to the view
            }
            return PartialView("_NoResults");
        }

        public async Task<IActionResult> Flight(string departure, string arrival, string status)
        {
            try
            {
                if (!string.IsNullOrEmpty(departure) && !string.IsNullOrEmpty(arrival))
                {
                    var flightQuery = _context.Flights.AsQueryable();

                    flightQuery = flightQuery.Where(f => f.DeparturePort == departure && f.ArrivalPort == arrival);

                    if (!string.IsNullOrEmpty(status))
                    {
                        flightQuery = flightQuery.Where(f => f.Status == status);
                    }

                    var flights = await flightQuery.ToListAsync();
                    return PartialView("_FlightResults", flights);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for flights.");
                // Optionally pass an error message or model to the view
            }
            return PartialView("_NoResults");
        }

        public async Task<IActionResult> Rental(string? vehicleName, string? vehicleType, string? state)
        {
            try
            {
                if (!String.IsNullOrEmpty(vehicleType) || !String.IsNullOrEmpty(vehicleName) || !String.IsNullOrEmpty(state))
                {
                    var rentalQuery = _context.Rentals.AsQueryable();

                    if (!String.IsNullOrEmpty(vehicleName))
                    {
                        rentalQuery = rentalQuery.Where(r => r.VehicleName == vehicleName);
                    }

                    if (!String.IsNullOrEmpty(vehicleType))
                    {
                        rentalQuery = rentalQuery.Where(r => r.VehicleType == vehicleType);
                    }

                    if (!String.IsNullOrEmpty(state))
                    {
                        rentalQuery = rentalQuery.Where(r => r.State == state);
                    }

                    var rentals = await rentalQuery.ToListAsync();
                    return PartialView("_RentalResults", rentals);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for rentals.");
                // Optionally pass an error message or model to the view
            }
            return PartialView("_NoResults");
        }
    }
}
