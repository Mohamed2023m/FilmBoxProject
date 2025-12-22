using FilmBox.Shared.DTOs.PostDTOs;

namespace FilmBox.Api.BusinessLogic
{
    public interface IAdminMediaLogic
    {
        Task<int> CreateMediaAsync(MediaCreateDto dto);
        Task UpdateImageAsync(int mediaId, IFormFile image);

    }
}
