using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    public class UserSession
    {
        public static bool IsLoggedIn { get; private set; }
        public static string UserName { get; private set; }
        public static string FirstName { get; private set; }

        public static void StartSession(string userName, string firstName)
        {
            UserName = userName;
            FirstName = firstName;
            IsLoggedIn = true;
        }

        public static void EndSession() 
        {
            UserName = null;
            FirstName = null;
            IsLoggedIn = false;
        }
    }
}
