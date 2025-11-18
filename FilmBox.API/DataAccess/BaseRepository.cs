using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace FilmBox.Api.BusinessLogic
{
    // This class provides the shared database logic
    public abstract class BaseRepository
    {
        protected readonly string _connectionString;

        protected BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        // Creates a new SQL connection on each request

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        protected async Task<bool> TryExecuteAsync<T>(string sql, T parameters)
        {


            try
            {
                using var conn = CreateConnection();
                
                var rowsAffected =  await conn.ExecuteAsync(sql, parameters);

                return rowsAffected > 0;

            }
            catch
            {
                return false;

            }
        } 
    }
}
