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

            //_trip.DepartureDate = _trip.DepartureDate.Date + chosenTime;
            _trip.ArrivalTime = new DateTime(_trip.ArrivalTime.Year, _trip.ArrivalTime.Month, _trip.ArrivalTime.Day,
                                       chosenTime.Hours, chosenTime.Minutes, 0);

            App.Current.Properties["ChosenTime"] = chosenTime;
            await Navigation.PushAsync(new DeparturePage(_trip));
        }
    }
}