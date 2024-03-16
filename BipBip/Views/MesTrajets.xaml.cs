using BipBip.Models;
using BipBip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BipBip.Models.Reservation;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MesTrajets : ContentPage
    {
        private readonly TripService _tripService;
        private readonly ReservationService _reservationService;
        private readonly DbService _dbService;

        public MesTrajets()
        {
            InitializeComponent();
            _tripService = new TripService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _reservationService = new ReservationService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));

            // Chargez les trajets réservés et publiés au démarrage de la page
            LoadReservedTrips();
            LoadPublishedTrips();
        }

        // Méthode pour charger les trajets réservés
        private void LoadReservedTrips()
        {
            var allReservedTrips = _reservationService.GetReservedTripsByUserId(UserSession.Id);
            foreach (var reservation in allReservedTrips)
            {
                reservation.Trip = _tripService.GetTripById(reservation.TripId);
                reservation.Trip.DriverName = _dbService.GetUserByIdAsync(reservation.Trip.DriverId).Result.Name;
                reservation.Trip.CarModel = _dbService.GetVehiculeByIdAsync(reservation.Trip.VehicleId).Result.Modele;
            }

            // Divisez les trajets réservés en deux sections : à venir et passés
            var now = DateTime.Now;
            var upcomingReservedTrips = allReservedTrips
                .Where(reservation => _tripService.GetTripById(reservation.TripId).DepartureTime > now)
                .ToList();
            
            var pastReservedTrips = allReservedTrips
                .Where(reservation => _tripService.GetTripById(reservation.TripId).DepartureTime <= now)
                .ToList();

            // Assigne les sources de données aux ListViews correspondantes
            PastReservedTripsCollectionView.ItemsSource = pastReservedTrips;
            ReservedUpcomingTripsCollectionView.ItemsSource = upcomingReservedTrips;
            
        }

        // Méthode pour charger les trajets publiés
        private void LoadPublishedTrips()
        {
            // Récupérez tous les trajets publiés par l'utilisateur actuel
            var allPublishedTrips = _tripService.GetPublishedTripsByUserId(UserSession.Id);
            foreach (var trip in allPublishedTrips)
            {
                trip.DriverName = _dbService.GetUserByIdAsync(trip.DriverId).Result.Name;
                trip.CarModel = _dbService.GetVehiculeByIdAsync(trip.VehicleId).Result.Modele;
            }

            // Divisez les trajets publiés en deux sections : à venir et passés
            var now = DateTime.Now;
            var upcomingPublishedTrips = allPublishedTrips.Where(trip => trip.DepartureTime > now).ToList();
            var pastPublishedTrips = allPublishedTrips.Where(trip => trip.DepartureTime <= now).ToList();

            // Assigne les sources de données aux ListViews correspondantes
            PastPublishedTripsCollectionView.ItemsSource = pastPublishedTrips;
            PublishedUpcomingTripsCollectionView.ItemsSource = upcomingPublishedTrips;
        }

        private async void OnRateButtonClicked(object sender, EventArgs e)
        {
            var selectedReservation = (Reservation)((Button)sender).BindingContext;

            await Navigation.PushAsync(new RatePage(selectedReservation));
        }

        private async void OnMessageButtonClicked(object sender, EventArgs e)
        {
            // Get the selected reservation from the binding context
            var selectedReservation = (Reservation)((Button)sender).BindingContext;

            await Navigation.PushAsync(new MessagePage());
        }
    }
}
