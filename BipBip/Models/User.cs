using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName {  get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string phoneNumber { get; set; }

        public double AverageRating { get; set; }

        public string ProfilePicturePath { get; set; } = "nabilz.png";

        public User() { }
    }
}
