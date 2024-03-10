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
    internal class TripService
    {
        private readonly TripRepo _tripRepository;

        public TripService(string databasePath)
        {
            _tripRepository = new TripRepo(databasePath);
        }

        public List<Trip> GetAllTrips()
        {
            return _tripRepository.GetAllTrips();
        }

        public Trip GetTripById(long id)
        {
            return _tripRepository.GetTripById(id);
        }

        public void AddTrip(Trip trip)
        {
            _tripRepository.InsertTrip(trip);
        }

        public void UpdateTrip(Trip trip)
        {
            _tripRepository.UpdateTrip(trip);
        }

        public void DeleteTrip(long id)
        {
            _tripRepository.DeleteTrip(id);
        }

        public List<Trip> SearchTrips(string departureCity, string destinationCity, DateTime departureDate, int numberOfSeats)
        {
            List<Trip> allTrips = _tripRepository.GetAllTrips();
            return allTrips
                .Where(trip =>
                    trip.Departure.Equals(departureCity, StringComparison.OrdinalIgnoreCase) &&
                    trip.Arrival.Equals(destinationCity, StringComparison.OrdinalIgnoreCase) &&
                    trip.DepartureTime.Date == departureDate.Date &&
                    trip.AvailableSeats >= numberOfSeats)
                .ToList();
        }

    }
}
