using MovieHub.API.Models;

namespace MovieHub.API.Data.Interfaces
{
    public interface IPostgresDataProvider
    {
        Task<IEnumerable<ShowDetailsDto>> GetActiveShowsAsync();
        Task<SpecificShowDetails> GetShowSeatDetailsAsync(int showId);
        Task<HoldSeatsResponse> HoldSeatsAsync(int showId, string username, string[] seats);
        Task<ConfirmBookingResponse> ConfirmBookingAsync(int showId, string username, string[] seats);
        Task ReleaseExpiredHoldsAsync();
        Task<BookingDetailsDto> GetBookingDetailsAsync(string username, string bookingReference);
        Task<IEnumerable<UserBookingDto>> GetUserBookingsAsync(string username);
    }
}
