using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DTOs;
using FilmBox.Api.DTOs.LoginDTO_s;
using Microsoft.AspNetCore.Mvc;

namespace FilmBox.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserLogic _logic;

        public UserController(UserLogic logic)
        {
            _logic = logic;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _logic.LoginAsync(request);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
