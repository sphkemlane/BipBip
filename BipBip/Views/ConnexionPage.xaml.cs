using BipBip.Models;
using BipBip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BipBip.Views
{
    public partial class ConnexionPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly DbService database;
        public ConnexionPage()
        {
            InitializeComponent();
            string databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3");
            _authService = new AuthService(databasePath);
            database = new DbService(databasePath);

            //var users = new User[]
            //{
            //    new User
            //    {
            //        Name = "KEMLANE",
            //        FirstName = "Saad",
            //        Email = "saad@mail.com",
            //        Password = "SecurePassword!", // Example, should be securely hashed
            //        phoneNumber = "0600000001",
            //        AverageRating = 4.5, // Randomly assigned
            //        ProfilePicturePath = "Saad.jpg"
            //    },
            //    new User
            //    {
            //        Name = "NKHILI",
            //        FirstName = "Nabil",
            //        Email = "nabil@mail.com",
            //        Password = "SecurePassword!",
            //        phoneNumber = "0600000002",
            //        AverageRating = 3.8, // Randomly assigned
            //        ProfilePicturePath = "Nabil.png"
            //    },
            //    new User
            //    {
            //        Name = "BEN ABDALLAH",
            //        FirstName = "Aymen",
            //        Email = "aymen@mail.com",
            //        Password = "SecurePassword!",
            //        phoneNumber = "0600000003",
            //        AverageRating = 4.2, // Randomly assigned
            //        ProfilePicturePath = "Aymen.jpg"
            //    },
            //    new User
            //    {
            //        Name = "ZAHIR",
            //        FirstName = "Mohamed",
            //        Email = "mohamed@mail.com",
            //        Password = "SecurePassword!",
            //        phoneNumber = "0600000004",
            //        AverageRating = 4.0, // Randomly assigned
            //        ProfilePicturePath = "Mohamed.jpg"
            //    },
            //    new User
            //    {
            //        Name = "KACEM",
            //        FirstName = "Labid",
            //        Email = "labid@mail.com",
            //        Password = "SecurePassword!",
            //        phoneNumber = "0600000005",
            //        AverageRating = 3.5, // Randomly assigned
            //        ProfilePicturePath = "Labid.jpg"
            //    }
            //};
            //foreach (var user in users)
            //{
            //    database.SaveUserAsync(user);
            //    Console.WriteLine($"User {user.FirstName} added successfully.");
            //}

            //string[] firstNames = { "Lucas", "Emma", "Hugo", "Chloé", "Louis", "Alice", "Jules", "Lina", "Gabriel", "Zoé" };
            //string[] lastNames = { "Martin", "Bernard", "Dubois", "Thomas", "Robert", "Richard", "Petit", "Durand", "Leroy", "Moreau" };

            //Random rnd = new Random();

            //for (int i = 0; i < 10; i++)
            //{
            //    var user = new User
            //    {
            //        Name = lastNames[rnd.Next(lastNames.Length)],
            //        FirstName = firstNames[rnd.Next(firstNames.Length)],
            //        Email = $"user{i}@mail.com",
            //        Password = $"SecurePassword!", // Example, should be securely hashed
            //        phoneNumber = $"0{rnd.Next(6, 8)}{rnd.Next(10000000, 99999999)}", // Generates phone numbers starting with 06 or 07
            //        AverageRating = rnd.NextDouble() * (5.0 - 1.0) + 1.0, // Generates a random rating between 1.0 and 5.0
            //    };

            //    database.SaveUserAsync(user);
            //    Console.WriteLine($"User {user.FirstName} {user.Name} added successfully, Id : {user.Id}");

            //}

            var userr = database.GetUserAsync();
            Console.WriteLine("Users added successfully." + userr.Result.Count);


        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            bool isAuthenticated = await _authService.AuthenticateAsync(email, password);

            if (isAuthenticated)
            {
                await Navigation.PushAsync(new HomePageF());
            }
            else
            {
                await DisplayAlert("erreur", "Identifiants incorrects", "OK");
            }
            
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegidterPage());
        }
    }
}

   