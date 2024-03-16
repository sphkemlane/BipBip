using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BipBip.Models;
using BipBip.Repositories;
using Newtonsoft.Json;

namespace BipBip.Services
{
    internal class ReservationService
    {
        private readonly ReservationRepo _reservationRepository;

        public ReservationService(string databasePath)
        {
            _reservationRepository = new ReservationRepo(databasePath);
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }

        public Reservation GetReservationById(long id)
        {
            return _reservationRepository.GetReservationById(id);
        }

        public void AddReservation(Reservation reservation)
        {
            _reservationRepository.InsertReservation(reservation);
        }

        public void RemoveReservation(Reservation reservation) 
        { 
            _reservationRepository.DeleteReservation(reservation.Id);
        }

        public List<Reservation> GetReservedTripsByUserId(int userId)
        {
            return _reservationRepository.GetReservedTripsByUserId(userId);
        }


       

        



    }
}
