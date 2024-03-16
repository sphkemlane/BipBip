using BipBip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BipBip.Models;
using Xamarin.Essentials;
using System.Security.Cryptography;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegidterPage : ContentPage
    {
        DbService database;

        public RegidterPage()
        {
            InitializeComponent();

            //initialisation mta3 el db
            database = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            Console.WriteLine(database);

        }

        private async void OnLabelTapped(object sender, EventArgs e) 
        {
            await Navigation.PushAsync(new ConnexionPage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Name = nameEntry.Text,
                FirstName = prenomEntry.Text,
                Email = emailEntry.Text,
                Password = passwordEntry.Text,
                phoneNumber = phoneEntry.Text
            };

            var existEmail = await database.GetUserByEmailAsync(user.Email);
            if (!verifEmail() || !verifNomPrenom() || !verifNumero())
                return;
            if (existEmail == null)
            {
                await database.SaveUserAsync(user);
                await DisplayAlert("Success", "Vous avez été inscris avec succès!", "OK");
                await DisplayAlert("mail", $"le mail :{user.Email} a été enregistré", "OK");
            }
            else
            {
                await DisplayAlert("Echec", "Cet email existe déjà!", "ERROR");
            }

            var users = await database.GetUserAsync();
            await DisplayAlert("Verif", $"{users.Count}", "Ok");
        }

        private bool verifEmail() 
        {
            if(!Regex.IsMatch(emailEntry.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                DisplayAlert("Error", "Veuillez saisir un email valide", "OK");
                return false;
            }
            return true;
        }
        private bool verifNomPrenom()
        {
            if (!Regex.IsMatch(prenomEntry.Text, @"^[a-zA-Z]") || !Regex.IsMatch(nameEntry.Text, @"^[a-zA-Z]"))
            {
                DisplayAlert("Error", "le format du prénom saisi est incorrect", "OK");
                return false;
            }
            return true;
        }

        private bool verifNumero()
        {
            if (!Regex.IsMatch(phoneEntry.Text, @"^[0-9]") || (phoneEntry.Text.Length > 10 || phoneEntry.Text.Length < 10))
            {
                DisplayAlert("Error", "veuillez saisir un numéro de téléphone correct", "OK");
                return false;
            }
            return true;
        }
    }

}
