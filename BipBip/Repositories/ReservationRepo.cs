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
    public class ReservationRepo
    {
        private readonly SQLiteConnection _database;

        public ReservationRepo(string databasePath)
        {
            _database = new SQLiteConnection(databasePath);
            _database.CreateTable<Reservation>();
        }

        public List<Reservation> GetAllReservations()
        {
            return _database.Table<Reservation>().ToList();
        }

        public Reservation GetReservationById(long id)
        {
            return _database.Table<Reservation>().FirstOrDefault(r => r.Id == id);
        }

        public void InsertReservation(Reservation reservation)
        {
            _database.Insert(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _database.Update(reservation);
        }

        public void DeleteReservation(long id)
        {
            _database.Delete<Reservation>(id);
        }

        public List<Reservation> GetReservedTripsByUserId(int userId)
        {
            return _database.Table<Reservation>().Where(r => r.ReserveeId == userId).ToList();
            
        }


    }
}
