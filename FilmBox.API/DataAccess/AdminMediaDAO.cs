using Dapper;
using FilmBox.Api.DataAccess;
using FilmBox.Api.Models;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    // Handles all SQL operations related to the Review entity
    public class AdminMediaDAO : BaseDAO, IAdminMediaDAO

    {
        public AdminMediaDAO(string connectionString)
    : base(connectionString)
        {
        }

        // SQL query for inserting a media
        private static readonly string InsertMediaSql = @"
            INSERT INTO Media (Title, Description, Genre, ImageUrl, MediaType, PublishDate)
            VALUES (@Title, @Description, @Genre, @ImageUrl, @MediaType, @PublishDate);
            SELECT CAST(SCOPE_IDENTITY() AS int);
        ";

                private const string UpdateImageSql = @"
            UPDATE Media
            SET ImageUrl = @ImageUrl
            WHERE MediaId = @MediaId;
        ";


        // Executes the SQL insert using BaseRepository helper.
        public async Task<int> InsertAsync(Media media)
        {
            using IDbConnection connection = CreateConnection();
            return await connection.QuerySingleAsync<int>(InsertMediaSql, media);
        }

        public async Task UpdateImageAsync(int mediaId, string imageUrl)
        {
            using IDbConnection connection = CreateConnection();
            await connection.ExecuteAsync(UpdateImageSql, new
            {
                MediaId = mediaId,
                ImageUrl = imageUrl
            });
        }
    }
}
