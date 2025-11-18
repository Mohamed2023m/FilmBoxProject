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

                var rowsAffected = await conn.ExecuteAsync(sql, parameters);

                return rowsAffected > 0;

            }
            catch
            {
                return false;

            }
        }
        // Vi opretter et Review og her er det samrtest at have et review Id med tilbage   
        // TryExecuteAsync = bruges til UPDATE/DELETE (kun "success / ikke success").
        // QuerySingleAsync<T> = bruges til INSERT + returnering af nyt ID.
        protected async Task<T> QuerySingleAsync<T>(string sql, object parameters)
        {
            using var conn = CreateConnection();
            return await conn.QuerySingleAsync<T>(sql, parameters);
        }
    }
}
