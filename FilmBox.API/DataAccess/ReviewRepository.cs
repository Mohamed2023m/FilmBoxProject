using Dapper;
using FilmBox.Api.Models;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    // Handles all SQL operations related to the Review entity
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        // Gets connection string from appsettings.json via IConfiguration
        public ReviewRepository(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentNullException("DefaultConnection is missing"))
        {
        }

        // SQL query for inserting a review
        private static readonly string InsertReviewSql = @"
        INSERT INTO Review (Rating, Description, MediaId, UserId)
        VALUES (@Rating, @Description, @MediaId, @UserId);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

        // Executes the SQL insert using BaseRepository helper.
        public async Task<bool> InsertAsync(Review review)
        {
            return  await TryExecuteAsync(InsertReviewSql, review); 

        }
    }
}
