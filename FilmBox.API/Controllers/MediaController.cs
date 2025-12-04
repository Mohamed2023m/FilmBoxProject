using FilmBox.API.BusinessLogic.Interfaces;
using FilmBox.API.DTOs.GetDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase // ← Missing inheritance
    {
        private readonly IMediaLogic _mediaLogic;

        public MediaController(IMediaLogic mediaLogic)
        {
            _mediaLogic = mediaLogic;
        }

        [HttpGet("Get-Media")]
        [ProducesResponseType(typeof(MediaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedia([FromBody] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("ID must be a positive integer");
                }

                var media = await _mediaLogic.GetMediaById(id);

                if (media == null)
                {
                    return NotFound($"Media with ID {id} not found");
                }

                return Ok(media);
            }
            catch (Exception ex)
            {
                // Log the exception here
                // _logger.LogError(ex, "Error retrieving media with ID {MediaId}", id);
                return StatusCode(500, "An error occurred while processing your request");
            }

        }


        [HttpGet("All-Films")]
        [ProducesResponseType(typeof(IEnumerable<MediaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFilms()
        {
            try
            {
                var mediaList = await _mediaLogic.GetAllFilms();

                // Optional: return 404 if no media exists
                if (mediaList == null || !mediaList.Any())
                {
                    return NotFound("No media found");
                }

                return Ok(mediaList);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error retrieving all media");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet("All-Series")]
        [ProducesResponseType(typeof(IEnumerable<MediaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSeries()
        {
            try
            {
                var mediaList = await _mediaLogic.GetAllSeries();

                // Optional: return 404 if no media exists
                if (mediaList == null || !mediaList.Any())
                {
                    return NotFound("No media found");
                }

                return Ok(mediaList);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error retrieving all media");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}