using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Models;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Controllers
{
    [Authorize]
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

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] ConfirmBookingRequest request)
        {
            var result = await _bookingService.ConfirmBookingAsync(request);
            return Ok(result);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetBookingDetails([FromQuery] string username, [FromQuery] string bookingReference)
        {
            var booking = await _bookingService.GetBookingDetailsAsync(username, bookingReference);

            if (booking == null)
                return NotFound(new { message = "Booking not found or not confirmed." });

            return Ok(booking);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserBookings([FromQuery] string username)
        {
            var bookings = await _bookingService.GetUserBookingsAsync(username);

            if (bookings == null || !bookings.Any())
                return NotFound(new { message = "No bookings found for this user." });

            return Ok(bookings);
        }
    }
}
