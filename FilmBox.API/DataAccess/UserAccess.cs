using Dapper;
using System.Data;
using FilmBox.Api.Models;

namespace FilmBox.Api.DataAccess
{
    public class UserAccess : BaseDAO, IUserAccess
    {
        public UserAccess(string connectionString)
            : base(connectionString)
        {
        }

        public async Task<User?> GetEmailAsync(string email)
        {
            const string sql = "SELECT * FROM [User] WHERE Email = @Email";

            using IDbConnection connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }
    }
}




