using Dapper;
using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
using FilmBox.Api.Utils;
using FilmBox.API.BusinessLogic.Interfaces;
using FilmBox.API.DataAccess.Interfaces;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;
     

        public ReviewService(IReviewRepository repo)
        {
            _repo = repo;
    
        }

        public async Task<bool> CreateReviewAsync(int? userId, ReviewCreateDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new System.ArgumentException("Rating must be between 1 and 5.");

 

            var review = new Review
            {
                Rating = dto.Rating,
                Description = dto.Description,
                MediaId = dto.MediaId,
                UserId = userId
            };

            var success = await _repo.InsertAsync(review);
            return success;
        }

        //public async Task<IEnumerable<ReviewDto>> GetReviewsForMediaAsync(int mediaId)
        //{
        //    var reviews = await _repo.GetByMediaIdAsync(mediaId);
        //    return reviews.ToDtos(); 
        //}

    }
}
