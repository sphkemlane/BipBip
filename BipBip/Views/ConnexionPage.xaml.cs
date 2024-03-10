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

        public ConnexionPage()
        {
            InitializeComponent();
            string databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3");
            _authService = new AuthService(databasePath);
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

   