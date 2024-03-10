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
    public partial class HomePageF : ContentPage
    {
        public HomePageF()
        {
            InitializeComponent();
        }

        private async void OnPublierButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page AddRide
            await Navigation.PushAsync(new AddRide());
        }

        private async void OnProfilButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page Profil
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void OnMesTrajetsButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page MesTrajets
            await Navigation.PushAsync(new MesTrajets());
        }

        private async void OnMessagesButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page MesReservations
            await Navigation.PushAsync(new MessagePage());
        }

        

        //deconnexion a mettre dans mon profil

        private void Deconnexion(object sender, EventArgs e)
        {
            UserSession.EndSession();
            Navigation.PopToRootAsync();
        }
    }
}