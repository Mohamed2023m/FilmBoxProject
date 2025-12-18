using FilmBox.Api.Models;

namespace FilmBox.Api.DataAccess
{
        // Defines all database operations related to Media
        public interface IAdminMediaDAO
    {
            // Inserts a new media into the database
            Task<int> InsertAsync(Media media);
        Task UpdateImageAsync(int mediaId, string imageUrl);

    }
}
