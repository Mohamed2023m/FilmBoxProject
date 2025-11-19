using Dapper;
using FilmBox.Api.DataAccess;
using FilmBox.Api.Models;
using FilmBox.API.DataAccess.Interfaces;
using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
       

        private static readonly string InsertReviewSql = @"
        INSERT INTO Review (Rating, Description, MediaId, UserId)
        VALUES (@Rating, @Description, @MediaId, @UserId);
        SELECT CAST(SCOPE_IDENTITY() AS int);";



        public ReviewRepository(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")
       ?? throw new ArgumentNullException("DefaultConnection is missing"))
        {
        }
        //private static readonly string SelectReviewsByMediaSql = @"
        //SELECT ReviewId, CreatedAt, Rating, Description, MediaId, UserId
        //FROM Review
        //WHERE MediaId = @MediaId
        //ORDER BY CreatedAt DESC;";

        /*
     * Vi skal ikke have denne metode da brugen ikke skal se sin review når han har lavet det, istedet vil
     * han have en anden section hvor han kan se alle reviews tilknyttet brugeren, så en metode der hedder getAllReviewsByUserId
     * vil være passende. 
     * og metode navn skal passe med det som du vil have udfører 
     * GetByMediaIdAsync og SelectReviewsByMediaSql passer ikke med hinanden 
     * 
     */


        //private static readonly string CheckMediaSql = @"
        //SELECT COUNT(1) 
        //FROM Media 
        //WHERE MediaId = @MediaId;";

        /*
         * Grunden til jeg har udkommenteret det her er fordi vi kun skal have en insert metode
         * vi har ikke brug for en metode til at checke en media eksisterer da det kræver
         * at brugeren skal trykke på filmen eller serien brugen vil reviewe
         * I den første load af appen hentes alle medier fra databasen og hvis 
         * mediet ikke eksisterer vil brugen ikke kunne se mediet og eller reviewe det
         */


        public async Task<bool> InsertAsync(Review review)
        {
            return  await TryExecuteAsync(InsertReviewSql, review); 

        }
        //public async Task<IEnumerable<Review>> GetByMediaIdAsync(int mediaId)
        //{
        //    EnsureOpen();
        //    return await _db.QueryAsync<Review>(SelectReviewsByMediaSql, new { MediaId = mediaId });

        //}

        //public async Task<bool> MediaExistsAsync(int mediaId)
        //{
        //    EnsureOpen();
        //    var count = await _db.ExecuteScalarAsync<int>(CheckMediaSql, new { MediaId = mediaId });
        //    return count > 0;
        //}
    }
}
