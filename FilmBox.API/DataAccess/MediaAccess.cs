

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

        private static readonly string fetchAllMedia = @" SELECT * FROM Media
       ;";

        public MediaAccess(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentNullException("DefaultConnection is missing"))
        { }


        public async Task<Media?> FetchMediaAsync(int id)
        {
            return await TryQuerySingleOrDefaultAsync<object, Media>(fetchMediaById, new { MediaId = id });

        }

        public async Task<IEnumerable<Media>> FetchAllMediaAsync()
        {
            return await TryQueryAsync<object, Media>(fetchAllMedia, new { });
        }


    }
}
