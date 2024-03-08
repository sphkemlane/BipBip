using BipBip.Models;
using BipBip.Services;
using BipBip.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            setWelcomeMessage();
           

            

            //-- 
            BindingContext = new ProfileViewModel(); // Assignation de votre ViewModel comme BindingContext

            MessagingCenter.Subscribe<AddBioPage, string>(this, "BioUpdated", (sender, bioText) =>
            {
                // Mettre à jour la bio sur la page de profil
                // Par exemple, en actualisant un label avec la nouvelle bio
                // bioLabel.Text = arg;
                Debug.WriteLine("Réception du message BioUpdated avec le texte: " + bioText);

                var viewModel = BindingContext as ProfileViewModel;
                if (viewModel != null)
                {
                    viewModel.UserBio = bioText;
                }
            });

        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Déterminez le chemin de la base de données
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Users.db3");
            var dbService = new DbService(dbPath);

            // Utilisez le nom d'utilisateur stocké dans la session pour récupérer l'email
            string username = UserSession.UserName; // Assurez-vous que UserSession.UserName est correctement initialisé
            var email = await dbService.GetUserEmailByUsernameAsync(username);

            if (!string.IsNullOrEmpty(email))
            {
                // Mettez à jour l'interface utilisateur avec l'email récupéré
                Device.BeginInvokeOnMainThread(() =>
                {
                    LabelEmail.Text = email;
                });
            }
        }



        private void setWelcomeMessage()
        {
            welcomeLabel.Text = $" {UserSession.FirstName} ";
            LabelNom.Text = $" {UserSession.UserName} ";
            LabelPrénom.Text = $" {UserSession.FirstName} ";
            LabelEmail.Text = $"   ";
        }

        

        //protected override void OnDisappearing()
        //{
        //    MessagingCenter.Unsubscribe<AddBioPage, string>(this, "BioUpdated");
        //    base.OnDisappearing();
        //}
    }
}