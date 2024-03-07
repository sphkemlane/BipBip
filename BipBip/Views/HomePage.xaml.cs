using BipBip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            setWelcomeMessage();
        }

        private void setWelcomeMessage()
        {
            welcomeLabel.Text = $"Bienvenue, {UserSession.FirstName} {UserSession.UserName} sur votre espace utilisateur";
        }

        private async void OnCTClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreferencesPage());
        }

        private async void OnAddCarClicked(object sender, EventArgs e)
        {
            Vehicule vehicule = new Vehicule();
            await Navigation.PushAsync(new AddCarPage1(vehicule));
        }

        private async void OnATClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRide());
        }
        private void Deconnexion(object sender, EventArgs e)
        {
            UserSession.EndSession();
            Navigation.PopToRootAsync();
        }
    }
}