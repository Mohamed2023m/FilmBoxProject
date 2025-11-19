using FilmBox.Api.Models;

namespace FilmBox.API.DataAccess.Interfaces
{
    public interface IMediaAccess
    {

        public  Task<Media?> FetchMediaAsync(int id);

        public Task<IEnumerable<Media>> FetchAllMediaAsync();
    }
}
