using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using FilmBox.Api.Models;
using FilmBox.Api.DataAccess.Exceptions;

namespace FilmBox.Api.DataAccess    
{
    public abstract class BaseRepository
    {
        //protected readonly IDbConnection _db;

        //protected BaseRepository(IDbConnection db)
        //{
        //    _db = db;
        //}

        //protected void EnsureOpen()
        //{
        //    if (_db.State != ConnectionState.Open)
        //        _db.Open();
        //}



        /*
        Grunden til jeg har udkommenteret dette kode er fordi, du åbner en connection, men den lukker aldrig
        Dapper håndtere allerede åbning og lukning af connection og hvis man også
        bruger using keyword smider den resourcen

        */
        protected readonly string _connectionString;

        protected BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }


        /*
        Det var mening at du skulle lave wrapper metoder som passer til de forskellige operationer
        */
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
        protected async Task<TResult?> TryQuerySingleOrDefaultAsync<TParams, TResult>(string sql, TParams parameters)

        {

            try
            {
                using var conn = CreateConnection();
                Console.WriteLine("Executing SQL: " + sql);

                var result = await conn.QuerySingleOrDefaultAsync<TResult>(sql, parameters);

                Console.WriteLine("Query executed successfully, result: " + (result == null ? "null" : result.ToString()));

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in repository: " + ex);
                throw;
            }

        }
        protected async Task<IEnumerable<TResult>> TryQueryAsync<TParams, TResult>(
     string sql,
     TParams parameters)
        {
            try
            {
                using var conn = CreateConnection();
                var result = await conn.QueryAsync<TResult>(sql, parameters);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Database query failed.", ex);
            }
        }




    }


}
