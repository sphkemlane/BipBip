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
    internal class RatingService
    {
        private readonly RatingRepo _ratingRepository;

        public RatingService(string databasePath)
        {
            _ratingRepository = new RatingRepo(databasePath);
        }

        public List<Rating> GetAllRatings()
        {
            return _ratingRepository.GetAllRatings();
        }

        public Rating GetRatingById(int id)
        {
            return _ratingRepository.GetRatingById(id);
        }

        public void AddRating(Rating rating)
        {
            _ratingRepository.InsertRating(rating);
        }

        public void UpdateRating(Rating rating)
        {
            _ratingRepository.UpdateRating(rating);
        }

        public void DeleteRating(int id)
        {
            _ratingRepository.DeleteRating(id);
        }

        public List<Rating> GetAllUserRatings(int userId)
        {
            return _ratingRepository.GetAllRatings().Where(rating => rating.UserId == userId).ToList();
        }
    }
}