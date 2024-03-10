using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using BipBip.Models;

namespace BipBip.Services
{
    internal class AuthService
    {
        private readonly DbService _dbService;

        public AuthService(string databasePath)
        {
            _dbService = new DbService(databasePath);
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            // Récupérer l'utilisateur avec l'email donné depuis la base de données
            User user = await _dbService.GetUserByEmailAsync(email);
            if (user != null && user.Password == password)
            {
                int id = user.Id;
                UserSession.StartSession(user.Name, user.FirstName, id);
                return true;
            }

            // si l'utilisateur n'existe pas on renvoie faux
            return false;
        }
    }
}

