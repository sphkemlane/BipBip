using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    internal class User
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName {  get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string phoneNumber { get; set; }
        public User() { }
    }
}
