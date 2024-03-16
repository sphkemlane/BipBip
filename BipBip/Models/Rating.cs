using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using SQLite;

namespace BipBip.Models
{
    [Table("Rating")]
    public class Rating
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public long TripId { get; set; }

        public int UserId { get; set; }

        public int RatingValue { get; set; }

        public string Comment { get; set; }

        public DateTime RatingDate { get; set; }

        public Rating()
        {
            RatingDate = DateTime.Now;
        }
    }
}


