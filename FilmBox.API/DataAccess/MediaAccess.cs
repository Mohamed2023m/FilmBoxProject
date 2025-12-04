

using FilmBox.Api.DataAccess;
using FilmBox.Api.Models;
using FilmBox.API.DataAccess.Interfaces;

namespace FilmBox.API.DataAccess
{
    public class MediaAccess : BaseRepository, IMediaAccess 
    {
        private static readonly string fetchMediaById = @" 
    SELECT MediaId, Title, Description, Genre, ImageUrl, PublishDate
    FROM Media WHERE MediaId = @MediaId;
";



        private static readonly string fetchMediaAllFilms = @" 
    SELECT MediaId, Title, ImageUrl 
    FROM Media WHERE MediaType = 'Movie';
";

        private static readonly string fetchMediaAllSeries = @" 
    SELECT MediaId, Title, ImageUrl 
    FROM Media WHERE MediaType = 'Series';
";



 

        public MediaAccess(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentNullException("DefaultConnection is missing"))
        { }


        public async Task<Media?> FetchMediaAsync(int id)
        {
            return await TryQuerySingleOrDefaultAsync<object, Media>(fetchMediaById, new { MediaId = id });

        }

        public async Task<IEnumerable<Media>> FetchAllFilmsAsync()
        {
            return await TryQueryAsync<object, Media>(fetchMediaAllFilms, new { });
        }




        public async Task<IEnumerable<Media>> FetchAllSeriesAsync()
        {
            return await TryQueryAsync<object, Media>(fetchMediaAllSeries, new { });
        }




    }
}
