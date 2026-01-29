using MovieHub.API.Data.Interfaces;
using MovieHub.API.Models;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Services
{
    public class BookingService : IBookingService
    {
        private readonly IPostgresDataProvider _dataProvider;

        public BookingService(IPostgresDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public Task<HoldSeatsResponse> HoldSeatsAsync(HoldSeatsRequest request)
        {
            return _dataProvider.HoldSeatsAsync(request.ShowId, request.Username, request.Seats);
        }
        public Task<ConfirmBookingResponse> ConfirmBookingAsync(ConfirmBookingRequest request)
        {
            return _dataProvider.ConfirmBookingAsync( request.ShowId, request.Username, request.Seats);
        }
    }
}
