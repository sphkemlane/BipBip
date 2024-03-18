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
    public partial class ComfortConfirmationPage : ContentPage
    {
        private Trip _trip;

        public ComfortConfirmationPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip; // Stocker l'objet Trip passé au constructeur
        }

        private async void OnAgreeClicked(object sender, System.EventArgs e)
        {
            _trip.LeaveSeatFree = true;
            // L'utilisateur est d'accord pour laisser un siège libre.
            App.Current.Properties["Comfort"] = true;

            // Passer à l'étape suivante.
            await Navigation.PushAsync(new InstantBookingPage(_trip));
        }

        private async void OnDisagreeClicked(object sender, System.EventArgs e)
        {
            _trip.LeaveSeatFree = false;
            // L'utilisateur n'est pas d'accord et préfère serrer les passagers.
            App.Current.Properties["Comfort"] = false;

            // Passer à l'étape suivante.
            await Navigation.PushAsync(new InstantBookingPage(_trip));
        }
    }
}