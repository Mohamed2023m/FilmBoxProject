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
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
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
    }
}