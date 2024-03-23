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

            foreach (var reservation in pastReservedTrips)
            {
                if (reservation.Rating != null && reservation.Rating != 0)
                {
                    reservation.RatingVisible = false; // Masquer le bouton
                }
                else
                {
                    reservation.RatingVisible = true; // Afficher le bouton
                }
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

            // coommenter juste pour tester il faut enlever les commentaires
            /*foreach (var trip in allPublishedTrips)
            {
                trip.DriverName = _dbService.GetUserByIdAsync(trip.DriverId).Result.Name;
                trip.CarModel = _dbService.GetVehiculeByIdAsync(trip.VehicleId).Result.Modele;
            }*/

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
            var discussion = await _dbService.GetDiscussionAsync(UserSession.Id, selectedReservation.Trip.DriverId);
            if (discussion == null)
            {
                Discussion newDiscussion = new Discussion
                {
                    UserIdSender = UserSession.Id,
                    UserIdReceiver = selectedReservation.Trip.DriverId,
                    UserName = _dbService.GetUserByIdAsync(selectedReservation.Trip.DriverId).Result.Name,
                };
                _dbService.SaveDiscussionAsync(newDiscussion);
                Console.WriteLine("Discussion Id: " + discussion.Id);
                await Navigation.PushAsync(new ChatDetailPage(newDiscussion));
            }
            
            await Navigation.PushAsync(new ChatDetailPage(discussion));

        }
        private void ShowPublishedUpcomingTrips(object sender, EventArgs e)
        {
            SetVisibility(PublishedUpcomingTripsCollectionView);
        }

        private void ShowPublishedArchivedTrips(object sender, EventArgs e)
        {
            SetVisibility(PastPublishedTripsCollectionView);
        }

        private void ShowReservedUpcomingTrips(object sender, EventArgs e)
        {
            SetVisibility(ReservedUpcomingTripsCollectionView);
        }

        private void ShowReservedArchivedTrips(object sender, EventArgs e)
        {
            SetVisibility(PastReservedTripsCollectionView);
        }

        private void SetVisibility(VisualElement elementToShow)
        {
            // Cachez toutes les CollectionViews
            PublishedUpcomingTripsCollectionView.IsVisible = false;
            PastPublishedTripsCollectionView.IsVisible = false;
            ReservedUpcomingTripsCollectionView.IsVisible = false;
            PastReservedTripsCollectionView.IsVisible = false;

            // Affichez seulement l'élément sélectionné
            elementToShow.IsVisible = true;
        }

    }
}