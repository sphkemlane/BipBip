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
    public partial class InstantBookingPage : ContentPage
    {
        private Trip _trip;

        public InstantBookingPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;
        }

        private async void OnOptionSelected(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            //bool instantBooking = button.Text == "Activer la réservation instantanée";
            //App.Current.Properties["InstantBooking"] = instantBooking;
            // Vous pouvez naviguer à la page suivante ou terminer le processus
            _trip.ReservationType = button.Text == "Activer la réservation instantanée" ? "Instant" : "OnApproval";


            await Navigation.PushAsync(new SetPricePage(_trip));
        }
    }
}