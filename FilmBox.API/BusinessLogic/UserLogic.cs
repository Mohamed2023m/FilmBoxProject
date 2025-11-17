using FilmBox.Api.Authentication;
using FilmBox.Api.DataAccess;
using FilmBox.Api.DTOs;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using FilmBox.Api.DTOs.LoginDTO_s;

namespace FilmBox.Api.BusinessLogic
{
    public class UserLogic:ControllerBase
    {
        private readonly IUserAccess _userAccess;
        private readonly JwtTokenGenerator _jwt;

        public UserLogic(IUserAccess userAccess, JwtTokenGenerator jwt)
        {
            _userAccess = userAccess;
            _jwt = jwt;
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var user = await _userAccess.GetEmailAsync(request.Email);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!validPassword)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var token = _jwt.GenerateToken(user);

            return new AuthResult
            {
                Success = true,
                Token = token,
                User = new UserDto
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }
    }
}
