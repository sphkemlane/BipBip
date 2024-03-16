using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BipBip.Models;
using BipBip.Services;

namespace BipBip.Views
{
    public partial class SearchResultsPage : ContentPage
    {
        int numberOfReservation;
        public SearchResultsPage(string departure, string destination, DateTime selectedDate, int numberOfPersons, List<Trip> matchingTrips)
        {
            numberOfReservation = numberOfPersons;
            InitializeComponent();

            // Initialiser l'interface utilisateur avec les valeurs de recherche
            DepartureLabel.Text = departure;
            DestinationLabel.Text = destination;
            DateLabel.Text = selectedDate.ToString("d");
            NumberOfPersonsLabel.Text = numberOfPersons.ToString();
            
            // Assurez-vous que votre CollectionView est liée à la liste matchingTrips
            MatchingTripsCollectionView.ItemsSource = matchingTrips;
        }

        private async void OnReserveButtonClicked(object sender, EventArgs e)
        {
            // Get the selected trip from the binding context
            var selectedTrip = (Trip)((Button)sender).BindingContext;

            // Navigate to the ReservationConfirmationPage with the selected trip information
            await Navigation.PushAsync(new ReservationConfirmationPage(selectedTrip, numberOfReservation));
        }





    }
}
