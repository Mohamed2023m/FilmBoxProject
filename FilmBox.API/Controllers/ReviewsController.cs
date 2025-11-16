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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto, [FromQuery] int userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //int? userId = null;
            /*
             * Du siger her at userId gerne må være null, med andre ord siger du at man ikke behøver være en bruger for at kunne lave en review
             */

            if (userId <= 0) return BadRequest(new { error = "UserId must be provided" });


            try
            {
                var success = await _service.CreateReviewAsync(userId, dto);

                return Ok(success);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { error = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        //// GET api/reviews/media/{mediaId}
        //[HttpGet("media/{mediaId}")]
        //public async Task<IActionResult> GetByMedia(int mediaId)
        //{
        //    try
        //    {
        //        var list = await _service.GetReviewsForMediaAsync(mediaId);
        //        return Ok(list);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = "An unexpected error occurred.", detail = ex.Message });
        //    }
        //}
    }
}