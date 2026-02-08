using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/shows")]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveShows()
        {
            var shows = await _showService.GetActiveShowsAsync();
            return Ok(shows);
        }


        [HttpGet("{showId:int}")]
        public async Task<IActionResult> GetShowDetails(int showId)
        {
            var result = await _showService.GetShowDetailsAsync(showId);

            if (result == null)
                return NotFound($"Show with id {showId} not found");

            return Ok(result);
        }
    }
}
