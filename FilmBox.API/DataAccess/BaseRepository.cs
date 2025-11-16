using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace FilmBox.Api.BusinessLogic
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
        Dapper håndtere allerede åbning og using keyword håndtere lukning af databasen

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
