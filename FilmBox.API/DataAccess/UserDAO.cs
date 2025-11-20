using Dapper;
using System.Data;
using FilmBox.Api.Models;

namespace FilmBox.Api.DataAccess
{
    public class UserDAO : BaseDAO, IUserDAO
    {
        public UserDAO(string connectionString)
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




