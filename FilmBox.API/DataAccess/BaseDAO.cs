using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace FilmBox.Api.BusinessLogic
{
    // This class provides the shared database logic
    public abstract class BaseDAO
    {
        protected readonly string _connectionString;

        protected BaseDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        // Creates a new SQL connection on each request

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        //TryExecuteAsync -> used for UPDATE or DELETE
        //Executes SQL commands that do not return data, but only modify rows in the database.
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
        // QuerySingleAsync -> used for INSERT + returning new ID
        // Executes SQL commands that return a single value, such as retrieving the ID of a newly inserted record.

        // Vi opretter et Review og her er det samrtest at have et review Id med tilbage   
        // TryExecuteAsync = bruges til UPDATE/DELETE (kun "success / ikke success").
        // QuerySingleAsync<T> = bruges til INSERT + returnering af nyt ID.
        protected async Task<T> QuerySingleAsync<T>(string sql, object parameters)
        {
            using var conn = CreateConnection();
            return await conn.QuerySingleAsync<T>(sql, parameters);
        }

        // QueryListAsync -> used for SELECT returning multiple rows
        // Executes a SQL query that returns a list of results.
        protected async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object parameters)
        {
            using var conn = CreateConnection();
            return await conn.QueryAsync<T>(sql, parameters);
        }
    }
}
