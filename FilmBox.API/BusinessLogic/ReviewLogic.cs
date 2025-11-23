using Dapper;
using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
//using FilmBox.Api.Utils;
using System.Data;
using WebApi.DTOs.Converters;

namespace FilmBox.Api.BusinessLogic
{
    // Handles validation, mapping, and uses the repository for database access
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewDAO _reviewDAO;

        // Repository is injected through constructor
        public ReviewLogic(IReviewDAO reviewDAO)
        {
            _reviewDAO = reviewDAO;
        }

        public async Task<int> CreateReviewAsync(int userId, ReviewCreateDto dto)
        {
            // Validate rating range
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new System.ArgumentException("Rating must be between 1 and 5.");

            // Build Review domain model from DTO.
            var review = new Review
            {
                Rating = dto.Rating,
                Description = dto.Description,
                MediaId = dto.MediaId,
                UserId = userId
            };

            // Insert into database through repository.
            var id = await _reviewDAO.InsertAsync(review);
            return id;
        }
        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserAsync(int userId)
        {
            if (userId <= 0) throw new System.ArgumentException("userId must be > 0");

            var reviews = await _reviewDAO.GetByUserIdAsync(userId);
            return reviews.ToDtos(); 
        }
    }
}
