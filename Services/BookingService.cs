using MovieHub.API.Data.Factory;
using MovieHub.API.Data.Interfaces;
using MovieHub.API.Models;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IPostgresDataProvider _dataProvider;

        public BookingService(DataProviderFactory factory)
        {
            _dataProvider = factory.Create();
        }
        public Task<HoldSeatsResponse> HoldSeatsAsync(HoldSeatsRequest request)
        {
            return _dataProvider.HoldSeatsAsync(request.ShowId, request.Username, request.Seats);
        }
        public Task<ConfirmBookingResponse> ConfirmBookingAsync(ConfirmBookingRequest request)
        {
            return _dataProvider.ConfirmBookingAsync( request.ShowId, request.Username, request.Seats);
        }
        public async Task<BookingDetailsDto> GetBookingDetailsAsync(string username, string bookingReference)
        {
            return await _dataProvider.GetBookingDetailsAsync(username, bookingReference);
        }
        public async Task<IEnumerable<UserBookingDto>> GetUserBookingsAsync(string username)
        {
            return await _dataProvider.GetUserBookingsAsync(username);
        }
    }
}
