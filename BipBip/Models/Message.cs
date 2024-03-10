using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    [Table("Message")]
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public int UserIdSender { get; set; }
        public int UserIdReceiver { get; set; }
    }
}
