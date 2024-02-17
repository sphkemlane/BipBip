using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_git.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public long UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserStatus Status { get; set; }
        public DateTime DateInscription { get; set; }

        public enum UserStatus
        {
            ACTIF,
            INACTIF,
            SUSPENDU
        }

        public User()
        {

        }
    }
}
