using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    [Table("Trip")]
    public class Trip
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public string Departure { get; set; }
        public string Arrival { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double Price { get; set; }
        public int AvailableSeats { get; set; }
        public string ReservationType { get; set; }

        [Ignore] 
        public string DriverName { get; set; }

        [Ignore]
        public string CarModel { get; set; }

        [Ignore]
        public User Driver { get; set; }

        public Trip()
        {

        }
    }
}
