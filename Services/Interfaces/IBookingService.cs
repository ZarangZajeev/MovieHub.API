using MovieHub.API.Models;

namespace MovieHub.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<HoldSeatsResponse> HoldSeatsAsync(HoldSeatsRequest request);
        Task<ConfirmBookingResponse> ConfirmBookingAsync(ConfirmBookingRequest request);
        Task<BookingDetailsDto> GetBookingDetailsAsync(string username, string bookingReference);
        Task<IEnumerable<UserBookingDto>> GetUserBookingsAsync(string username);
    }
}
