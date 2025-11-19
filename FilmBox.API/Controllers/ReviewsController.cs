using Microsoft.AspNetCore.Mvc;
using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.DTOs.GetDTOs;
using System;
using System.Threading.Tasks;

namespace FilmBox.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewLogic _service;

        public ReviewsController(IReviewLogic service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // POST api/reviews
        // Creates a new review
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto, [FromQuery] int userId)
        {
            // Validate model in ReviewCreateDto

            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Simulated userId (in a real app, this would come from authentication context, waiting for login user)
            if (userId <= 0) return BadRequest(new { error = "UserId must be provided" });


            try
            {
                // Attempt to create the review using the service layer
                var id = await _service.CreateReviewAsync(userId, dto);

                return Ok(id);
            }
            catch (ArgumentException ex)
            {
                // Used for validation errors
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Used when a related entity is missing
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch - all for unexpected exceptions
                return StatusCode(500, new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        // GET api/reviews/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            if (userId <= 0) return BadRequest(new { error = "userId must be > 0" });

            try
            {
                var list = await _service.GetReviewsByUserAsync(userId);
                return Ok(list); // 200 OK + JSON array
            }
            catch (ArgumentException ex)
            {
                // Used for validation errors
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Used when a related entity is missing
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch - all for unexpected exceptions

                return StatusCode(500, new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }
    }
}

