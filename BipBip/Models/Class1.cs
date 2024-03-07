using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using SQLite;

namespace BipBip.Models
{

    public class CarpoolPreference
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool Smoking { get; set; }
        public bool Music { get; set; }
        public bool Conversation { get; set; }
        public bool Eating { get; set; }
    }
}


