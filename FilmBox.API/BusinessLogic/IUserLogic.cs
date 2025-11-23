using FilmBox.Api.DTOs;
using FilmBox.Api.DTOs.LoginDTO_s;

namespace FilmBox.Api.BusinessLogic
{
    public interface IUserLogic
    {
        Task<AuthResult> LoginAsync(LoginRequest request);
    }
}
