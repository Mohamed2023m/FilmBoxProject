using FilmBox.Api.BusinessLogic;
using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmBox.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewLogic _logic;

        public ReviewsController(IReviewLogic logic)
        {
            _logic = logic;
        }

        // POST api/reviews
        // Creates a new review
        
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
        {
            // Validate model in ReviewCreateDto

            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Extract userId from JWT token
            var claim = User.FindFirst("userId") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || !int.TryParse(claim.Value, out var userId))
                return Unauthorized(new { error = "Missing userId in token" });

            //int userId = int.Parse(userID_JWT.Value);

            try
            {
                // Attempt to create the review using the service layer
                var id = await _logic.CreateReviewAsync(userId, dto);

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
                var list = await _logic.GetReviewsByUserAsync(userId);
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

