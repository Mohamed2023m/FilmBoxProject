using Dapper;
using FilmBox.Api.DataAccess;
using FilmBox.Api.Models;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    // Handles all SQL operations related to the Review entity
    public class ReviewDAO : BaseDAO, IReviewDAO

    {
        public ReviewDAO(string connectionString)
    : base(connectionString)
        {
        }

        // SQL query for inserting a review
        private static readonly string InsertReviewSql = @"
        INSERT INTO Review (Rating, Description, MediaId, UserId)
        VALUES (@Rating, @Description, @MediaId, @UserId);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

        private static readonly string SelectReviewsByUserSql = @"
        SELECT ReviewId, CreatedAt, Rating, Description, MediaId, UserId
        FROM Review
        WHERE UserId = @UserId
        ORDER BY CreatedAt DESC;";

        // Executes the SQL insert using BaseRepository helper.
        public async Task<int> InsertAsync(Review review)
        {

           using IDbConnection connection = CreateConnection();
            var id = await connection.QuerySingleAsync<int>(InsertReviewSql, review);
            return id;


        
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(int userId)
        {
            using IDbConnection connection = CreateConnection();

            return await connection.QueryAsync<Review>(SelectReviewsByUserSql, new { UserId = userId });
        }
    }
}
