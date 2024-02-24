using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assign1.Models
{
    public class Flight
    {
        [Required]
        public int FlightId { get; set; }

        [Required]
        public required string AirlineName { get; set; }

        [Required]
        public required string DeparturePort { get; set; }

        [Required]
        public required string ArrivalPort { get; set; }

        [Required]
        public int AvailSeats { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketCost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public required string Status { get; set; }

        [Required]
        public int FlightDuration { get; set; }
    }
}

