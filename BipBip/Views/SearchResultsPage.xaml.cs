using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BipBip.Models;
using BipBip.Services;
using System.Linq;

namespace BipBip.Views
{
    public partial class SearchResultsPage : ContentPage
    {
        int numberOfReservation;
        List<Trip> sortedTrips;
        string depart, destin;
        DateTime date;
        private readonly DbService _dbService;
        public SearchResultsPage(string departure, string destination, DateTime selectedDate, int numberOfPersons, List<Trip> matchingTrips)
        {
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            numberOfReservation = numberOfPersons;
            sortedTrips = matchingTrips;
            depart = departure;
            destin = destination;
            date = selectedDate;

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

        private void OnFilterButtonClicked(object sender, EventArgs e)
        {
            if (FilterFrame.IsVisible)
            {
                FilterFrame.IsVisible = false;
            }
            else
            {
                FilterFrame.IsVisible = true;
            }
        }

        private void OnRatingFilterClicked(object sender, EventArgs e)
        {
            // Appliquer le filtrage par rating du mieux noté au moins noté
            foreach (var trip in sortedTrips)
            {
                trip.Driver = _dbService.GetUserByIdAsync(trip.DriverId).Result;
            }
            sortedTrips = sortedTrips.OrderByDescending(trip => trip.Driver.AverageRating).ToList();

            Navigation.PushAsync(new SearchResultsPage(depart, destin, date, numberOfReservation, sortedTrips));

        }

        private void OnPriceFilterClicked(object sender, EventArgs e)
        {
            // Appliquer le filtrage par prix le plus bas au plus haut
            sortedTrips = sortedTrips.OrderBy(trip => trip.Price).ToList();

            Navigation.PushAsync(new SearchResultsPage(depart, destin, date, numberOfReservation, sortedTrips));


        }

        private void OnDepartureTimeFilterClicked(object sender, EventArgs e)
        {
            // Appliquer le filtrage par départ plus tôt
            sortedTrips = sortedTrips.OrderBy(trip => trip.DepartureTime).ToList();

            Navigation.PushAsync(new SearchResultsPage(depart, destin, date, numberOfReservation, sortedTrips));

        }





    }
}
