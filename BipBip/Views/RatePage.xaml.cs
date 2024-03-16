using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BipBip.Models;
using BipBip.Services;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatePage : ContentPage
    {
        private readonly ReservationService _reservationService;
        private readonly TripService _tripService;
        private readonly RatingService _ratingService;
        private readonly DbService _dbService;
        private int rating = 0;
        private Reservation reservation;
        //private StackLayout starsLayout;
        public RatePage(Reservation selectedReservation)
        {
            InitializeComponent();
            _reservationService = new ReservationService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _tripService = new TripService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _ratingService = new RatingService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            reservation = selectedReservation;
        }
        private void OnStarTapped(object sender, EventArgs e)
        {
            Image clickedStar = sender as Image;

            // Get the index of the clicked star
            int clickedIndex = int.Parse(clickedStar.ClassId);

            // Update the rating based on the clicked star
            rating = clickedIndex;

            // Update the UI to reflect the selected rating
            UpdateRatingUI();
        }

        private void UpdateRatingUI()
        {
            // Loop through each star image and update its source
            for (int i = 1; i <= 5; i++)
            {
                string starName = $"star{i}";

                Image starImage = this.FindByName<Image>(starName);

                if (starImage != null)
                {
                    if (i <= rating)
                    {
                        starImage.Source = "star_yellow.png"; // Selected star image
                    }
                    else
                    {
                        starImage.Source = "star_gray.png"; // Unselected star image
                    }
                }
            }
        }

        private void OnEvaluateButtonClicked(object sender, EventArgs e)
        {
            Rating _rating = new Rating
            {
                TripId = reservation.TripId,
                UserId = reservation.ReserveeId,
                RatingValue = rating,
                Comment = CommentEntry.Text

            };

            _ratingService.AddRating(_rating);
            

            reservation.Rating = rating;
            reservation.Comment = CommentEntry.Text;
            _reservationService.UpdateReservation(reservation);


            // ajouter le rate a average rating
            var ratingList = _ratingService.GetAllUserRatings(reservation.Trip.DriverId);

            
            User driver = _dbService.GetUserByIdAsync(reservation.Trip.DriverId).Result;

            // Vérifier si le conducteur a des évaluations
            if (ratingList.Any())
            {
                // Calculer la moyenne des évaluations
                double averageRating = ratingList.Average(rating => rating.RatingValue);

                // Mettre à jour la propriété averageRating du conducteur
                driver.AverageRating = averageRating;

                // Mettre à jour le conducteur dans la base de données
                _dbService.UpdateUserAsync(driver);
            }

            // Perform evaluation based on the selected rating
            DisplayAlert("Evaluation", $"Vous avez noté cette réservation avec {rating} étoiles.", "OK");
            Navigation.PushAsync(new HomePageF());
        }
    }

}
