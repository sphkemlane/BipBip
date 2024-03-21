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
    public partial class TimePickerPage : ContentPage
    {
        private Trip _trip;
        public TimePickerPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void OnContinueClicked(object sender, EventArgs e)
        {
            // Sauvegarder l'heure choisie et aller à la page suivante
            TimeSpan chosenTime = timePicker.Time;

            TimeSpan chosenArrivalTime = arrivalTimePicker.Time;

            
            _trip.DepartureTime = new DateTime(_trip.DepartureTime.Year, _trip.DepartureTime.Month, _trip.DepartureTime.Day,
                                       chosenTime.Hours, chosenTime.Minutes, 0);

            _trip.ArrivalTime = new DateTime(_trip.DepartureTime.Year, _trip.DepartureTime.Month, _trip.DepartureTime.Day,
                                       chosenArrivalTime.Hours, chosenArrivalTime.Minutes, 0);



            App.Current.Properties["ChosenTime"] = chosenTime;
            App.Current.Properties["chosenArrivalTime"] = chosenArrivalTime;

            // Assurez-vous de sauvegarder les propriétés après les avoir mises à jour
            await App.Current.SavePropertiesAsync();

            await Navigation.PushAsync(new DeparturePage(_trip));
        }
    }
}