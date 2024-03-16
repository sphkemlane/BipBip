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
    public class RatingRepo
    {
        private readonly SQLiteConnection _database;

        public RatingRepo(string databasePath)
        {
            _database = new SQLiteConnection(databasePath);
            _database.CreateTable<Rating>();
        }

        public List<Rating> GetAllRatings()
        {
            return _database.Table<Rating>().ToList();
        }

        public Rating GetRatingById(int id)
        {
            return _database.Table<Rating>().FirstOrDefault(r => r.Id == id);
        }

        public void InsertRating(Rating rating)
        {
            _database.Insert(rating);
        }

        public void UpdateRating(Rating rating)
        {
            _database.Update(rating);
        }

        public void DeleteRating(int id)
        {
            _database.Delete<Rating>(id);
        }
    }
}