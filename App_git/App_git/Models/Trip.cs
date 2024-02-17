using SQLite;
using System;
using SQLiteNetExtensions;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace App_git.Models
{
    public class Trip
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [ForeignKey(typeof(User))]
        public long DriverId { get; set; }

        [ForeignKey(typeof(Vehicle))]
        public long VehicleId { get; set; }

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
        public User Driver { get; set; }

        [Ignore]
        public Vehicle Vehicle { get; set; }

        public Trip()
        {

        }
    }
}
