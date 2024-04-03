using Assign1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assign1.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> HotelSearch(string destination, int rating, DateTime? checkIn, DateTime? checkOut)
        {
            if (!String.IsNullOrEmpty(destination))
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

                var hotels = await hotelQuery.ToListAsync();
                return PartialView("_HotelResults", hotels);
            }
            return PartialView("_NoResults");
        }


        public async Task<IActionResult> Flight(string departure, string arrival)
        {
            if (!String.IsNullOrEmpty(departure) && !String.IsNullOrEmpty(arrival))
            {
                // Ensure the query itself is not executed until the ToListAsync call
                var flightQuery = _context.Flights.AsQueryable();

                if (!String.IsNullOrEmpty(departure))
                {
                    flightQuery = flightQuery.Where(f => f.DeparturePort == departure);
                }

                if (!String.IsNullOrEmpty(arrival))
                {
                    flightQuery = flightQuery.Where(f => f.ArrivalPort == arrival);
                }

                // Execute the query asynchronously
                var flights = await flightQuery.ToListAsync();
                return PartialView("_FlightResults", flights);
            }

            // You could also consider making a "NoResults" view async if there's database interaction
            return PartialView("_NoResults");
        }

        public async Task<IActionResult> Rental(string? vehicleName, string? vehicleType, string? state)
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

            return PartialView("_NoResults");
        }
    }
}

