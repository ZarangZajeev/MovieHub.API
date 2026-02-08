using MovieHub.API.Data.Interfaces;
using MovieHub.API.Models;

namespace MovieHub.API.Data
{
    public class SqlDataProvider : IPostgresDataProvider
    {
        private readonly string _connectionString;

        public SqlDataProvider(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("connectionDb");
        }

        public Task<ConfirmBookingResponse> ConfirmBookingAsync(int showId, string username, string[] seats)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShowDetailsDto>> GetActiveShowsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookingDetailsDto> GetBookingDetailsAsync(string username, string bookingReference)
        {
            throw new NotImplementedException();
        }

        public Task<SpecificShowDetails> GetShowSeatDetailsAsync(int showId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserBookingDto>> GetUserBookingsAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<HoldSeatsResponse> HoldSeatsAsync(int showId, string username, string[] seats)
        {
            throw new NotImplementedException();
        }

        public Task ReleaseExpiredHoldsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsAsync(string email, string googleid)
        {
            throw new NotImplementedException();
        }
    }
}
