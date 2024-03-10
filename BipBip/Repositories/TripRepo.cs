using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using BipBip.Models;
using SQLite;
using System.Threading.Tasks;

namespace BipBip.Repositories
{
    public class TripRepo
    {
        private readonly SQLiteConnection _database;

        public TripRepo(string databasePath)
        {
            _database = new SQLiteConnection(databasePath);
            _database.CreateTable<Trip>();
        }

        public List<Trip> GetAllTrips()
        {
            return _database.Table<Trip>().ToList();
        }

        public Trip GetTripById(long id)
        {
            return _database.Table<Trip>().FirstOrDefault(t => t.Id == id);
        }

        public void InsertTrip(Trip trip)
        {
            _database.Insert(trip);
        }

        public void UpdateTrip(Trip trip)
        {
            _database.Update(trip);
        }

        public void DeleteTrip(long id)
        {
            _database.Delete<Trip>(id);
        }
    }
}
