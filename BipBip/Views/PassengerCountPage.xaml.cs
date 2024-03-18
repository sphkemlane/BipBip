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
    public partial class PassengerCountPage : ContentPage
    {
        private Trip _trip;
        public PassengerCountPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;
        }
        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            // La nouvelle valeur est déjà bindée via le XAML au Label.
        }

        private async void OnContinueClicked(object sender, System.EventArgs e)
        {
            // Sauvegarder le nombre de passagers choisi et passer à l'étape suivante.
            var passengerCount = (int)passengerStepper.Value;

            _trip.AvailableSeats = passengerCount;

            App.Current.Properties["PassengerCount"] = passengerCount;

            // Naviguer à la page suivante, par exemple la page de confirmation de confort.
            await Navigation.PushAsync(new ComfortConfirmationPage(_trip));
        }

    }
}