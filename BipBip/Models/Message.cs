using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }

        public Message()
        {
        }

        public Message(string content)
        {
            Content = content;
        }
    }
}
