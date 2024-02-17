using System;
using SQLite;
using SQLiteNetExtensions;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions.Attributes;

namespace App_git.Models
{
    public class Message
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }

        [ForeignKey(typeof(User))]
        public long SenderId { get; set; }

        [ForeignKey(typeof(User))]
        public long ReceiverId { get; set; }

        [Ignore]
        public User Sender { get; set; }
        [Ignore]
        public User Receiver { get; set; }

        public Message()
        {
        }

        public Message(string content)
        {
            Content = content;
        }
    }
}
