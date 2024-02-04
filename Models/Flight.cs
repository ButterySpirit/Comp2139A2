using System;
using System.ComponentModel.DataAnnotations;

namespace Assign1.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        public required string AirlineName { get; set; }

        public required string DeparturePort { get; set; }

        public required string ArrivalPort { get; set; }

        public int AvailSeats { get; set; }

        public int TicketCost { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public required string Status { get; set; }

        public int FlightDuration { get; set; }
    }
}

