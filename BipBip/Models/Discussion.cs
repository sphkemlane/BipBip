using SQLite;
using System;
using System.Collections.Generic;

namespace BipBip.Models
{
    [Table("Discussion")]
    public class Discussion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime LastMessageDate { get; set; }

        [MaxLength(250)]
        public string LastMessagePreview { get; set; }

        public string LastMessageStatus { get; set; } // e.g., sent, received, read

        public int UnreadMessageCount { get; set; }

        // The ID of the user the conversation is with.
        public int UserIdSender { get; set; }
        public int UserIdReceiver { get; set; }

        // Assuming you also want to show the user's name and profile picture in the chat list
        [MaxLength(100)]
        public string UserName { get; set; }

        [MaxLength(255)]
        public string UserProfilePicturePath { get; set; }

        [Ignore]
        public List<Message> Messages { get; set; } 
    }
}
