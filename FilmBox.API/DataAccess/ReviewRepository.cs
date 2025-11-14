using Dapper;
using FilmBox.Api.Models;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(IDbConnection db) : base(db)
        {
        }

        private static readonly string InsertReviewSql = @"
        INSERT INTO Review (Rating, Description, MediaId, UserId)
        VALUES (@Rating, @Description, @MediaId, @UserId);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

        private static readonly string SelectReviewsByMediaSql = @"
        SELECT ReviewId, CreatedAt, Rating, Description, MediaId, UserId
        FROM Review
        WHERE MediaId = @MediaId
        ORDER BY CreatedAt DESC;";

        private static readonly string CheckMediaSql = @"
        SELECT COUNT(1) 
        FROM Media 
        WHERE MediaId = @MediaId;";


        public async Task<int> InsertAsync(Review review)
        {
            EnsureOpen();
            return await _db.QuerySingleAsync<int>(InsertReviewSql, new { review.Rating, review.Description, review.MediaId, review.UserId });

        }
        public async Task<IEnumerable<Review>> GetByMediaIdAsync(int mediaId)
        {
            EnsureOpen();
            return await _db.QueryAsync<Review>(SelectReviewsByMediaSql, new { MediaId = mediaId });

        }

        public async Task<bool> MediaExistsAsync(int mediaId)
        {
            EnsureOpen();
            var count = await _db.ExecuteScalarAsync<int>(CheckMediaSql, new { MediaId = mediaId });
            return count > 0;
        }
    }
}
