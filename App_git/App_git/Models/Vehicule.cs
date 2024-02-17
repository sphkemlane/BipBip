using SQLite;
using System;
using SQLiteNetExtensions;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace App_git.Models
{
    public class Vehicle
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [ForeignKey(typeof(User))]
        public long OwnerId { get; set; }

        public string NumImmc { get; set; }
        public string VehicleType { get; set; }
        public string VehicleMarque { get; set; }
        public string VehicleModele { get; set; }
        public string VehicleColor { get; set; }
        public int AnneeImmc { get; set; }
        public bool AirConditioning { get; set; }

        [Ignore]
        public User Owner { get; set; }

        public Vehicle()
        {

        }
    }
}
