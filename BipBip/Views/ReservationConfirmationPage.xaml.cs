using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BipBip.Models;
using BipBip.Services;

namespace BipBip.Views
{
    public partial class ReservationConfirmationPage : ContentPage
    {
        private readonly ReservationService _reservationService;
        private readonly TripService _tripService;

        Trip trip;
        int numberOfReservation;
        public ReservationConfirmationPage(Trip selectedTrip, int nbOfReservation)
        {
            InitializeComponent();

            BindingContext = selectedTrip;
            numberOfReservation = nbOfReservation;
            trip = selectedTrip;
            _reservationService = new ReservationService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _tripService = new TripService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));

        }

        private void OnConfirmReservationClicked(object sender, EventArgs e)
        {
            Reservation.ReservationStatus reservationStatus = Reservation.ReservationStatus.Confirmed; ;
            bool paymentSituation = true;
            if (trip.ReservationType == "WaitingForConfirmation")
            {
                reservationStatus = Reservation.ReservationStatus.WaitingForConfirmation;
                paymentSituation = false;
            }
            
            Console.WriteLine("TripId: " + trip.Id);
            Reservation newReservation = new Reservation
            {
                TripId = trip.Id,
                ReserveeId = UserSession.Id,
                ReservationDate = DateTime.Now,
                PaymentSituation = paymentSituation,
                NumberOfReservation = numberOfReservation,
                Status = reservationStatus,
                Type = Reservation.ServiceType.Normal,
                Trip = trip,
            };
            trip.AvailableSeats -= numberOfReservation;
            _tripService.UpdateTrip(trip);

            _reservationService.AddReservation(newReservation);

            

            DisplayAlert("Success", "Reservation confirmed successfully! : ", "OK");

        }
    }

    }
