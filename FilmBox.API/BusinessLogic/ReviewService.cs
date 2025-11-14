using Dapper;
using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
using FilmBox.Api.Utils;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;
        private readonly IDbConnection _db;

        public ReviewService(IReviewRepository repo, IDbConnection db)
        {
            _repo = repo;
            _db = db;
        }

        public async Task<int> CreateReviewAsync(int? userId, ReviewCreateDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                throw new System.ArgumentException("Rating must be between 1 and 5.");

            const string checkMediaSql = "SELECT COUNT(1) FROM Media WHERE MediaId = @MediaId;";
            if (_db.State != ConnectionState.Open) _db.Open();
            var exists = await _db.ExecuteScalarAsync<int>(checkMediaSql, new { dto.MediaId });
            if (exists == 0) throw new System.InvalidOperationException("Media not found.");

            var review = new Review
            {
                Rating = dto.Rating,
                Description = dto.Description,
                MediaId = dto.MediaId,
                UserId = userId
            };

            var newId = await _repo.InsertAsync(review);
            return newId;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsForMediaAsync(int mediaId)
        {
            var reviews = await _repo.GetByMediaIdAsync(mediaId);
            return reviews.ToDtos(); 
        }

    }
}
