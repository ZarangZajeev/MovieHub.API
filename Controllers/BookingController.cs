using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Models;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("hold")]
        public async Task<IActionResult> HoldSeats([FromBody] HoldSeatsRequest request)
        {
            var result = await _bookingService.HoldSeatsAsync(request);
            return Ok(result);
        }
    }
}
