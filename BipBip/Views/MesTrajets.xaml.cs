using BipBip.Models;
using BipBip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MesTrajets : ContentPage
    {
        private readonly TripService _tripService;
        private readonly ReservationService _reservationService;

        public MesTrajets()
        {
            InitializeComponent();
            _tripService = new TripService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _reservationService = new ReservationService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));

            // Chargez les trajets réservés et publiés au démarrage de la page
            LoadReservedTrips();
            LoadPublishedTrips();
        }

        // Méthode pour charger les trajets réservés
        private void LoadReservedTrips()
        {
            // Récupérez tous les trajets réservés pour l'utilisateur actuel
            var allReservedTrips = _reservationService.GetReservedTripsByUserId(UserSession.Id);
            Console.WriteLine("count: all:  " + allReservedTrips.Count);

            // Divisez les trajets réservés en deux sections : à venir et passés
            var now = DateTime.Now;
            var upcomingReservedTrips = allReservedTrips
                .Where(reservation => _tripService.GetTripById(reservation.TripId).DepartureTime > now)
                .ToList();
            foreach (var reservation in upcomingReservedTrips)
            {
                reservation.Trip = _tripService.GetTripById(reservation.TripId);
            }
            var pastReservedTrips = allReservedTrips
                .Where(reservation => _tripService.GetTripById(reservation.TripId).DepartureTime <= now)
                .ToList();
            foreach (var reservation in pastReservedTrips)
            {
                reservation.Trip = _tripService.GetTripById(reservation.TripId);
            }

            // Assigne les sources de données aux ListViews correspondantes
            PastReservedTripsCollectionView.ItemsSource = pastReservedTrips;
            ReservedUpcomingTripsCollectionView.ItemsSource = upcomingReservedTrips;
            
        }

        // Méthode pour charger les trajets publiés
        private void LoadPublishedTrips()
        {
            // Récupérez tous les trajets publiés par l'utilisateur actuel
            var allPublishedTrips = _tripService.GetPublishedTripsByUserId(UserSession.Id);

            // Divisez les trajets publiés en deux sections : à venir et passés
            var now = DateTime.Now;
            var upcomingPublishedTrips = allPublishedTrips.Where(trip => trip.DepartureTime > now).ToList();
            var pastPublishedTrips = allPublishedTrips.Where(trip => trip.DepartureTime <= now).ToList();

            // Assigne les sources de données aux ListViews correspondantes
            PastPublishedTripsCollectionView.ItemsSource = pastPublishedTrips;
            PublishedUpcomingTripsCollectionView.ItemsSource = upcomingPublishedTrips;
        }
    }
}
