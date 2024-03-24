using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BipBip.Services
{
    public static class SessionService
    {
        // Propriété statique pour stocker la biographie de l'utilisateur
        public static string UserBio { get; set; }

        // Propriété statique pour stocker l'image de profil de l'utilisateur
        public static String UserProfileImage { get; set; }

        // Vous pouvez ajouter d'autres propriétés statiques si nécessaire pour stocker plus de données de session


    }
}