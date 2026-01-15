using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
using FilmBox.Shared.DTOs.PostDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmBox.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminMediaController : ControllerBase
    {
        private readonly IAdminMediaLogic _adminMediaLogic;

        public AdminMediaController(IAdminMediaLogic adminMediaLogic)
        {
            _adminMediaLogic = adminMediaLogic;
        }

        // POST api/Media
        // Creates a new Media

        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MediaCreateDto dto)
        {
            // Validate model in ReviewCreateDto

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // Attempt to create the media using the service layer
                var id = await _adminMediaLogic.CreateMediaAsync(dto);

                return Ok(id);
            }
            catch (ArgumentException ex)
            {
                // Used for validation errors
                return BadRequest(new { error = ex.Message });
            }

            catch (Exception ex)
            {
                // Catch - all for unexpected exceptions
                return StatusCode(500, new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }
        [HttpPut("{id}/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage(int id,IFormFile image)
        {
            await _adminMediaLogic.UpdateImageAsync(id, image);
            return NoContent();
        }
    }
}


