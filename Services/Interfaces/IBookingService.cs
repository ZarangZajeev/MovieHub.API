using MovieHub.API.Models;

namespace MovieHub.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<HoldSeatsResponse> HoldSeatsAsync(HoldSeatsRequest request);
        Task<ConfirmBookingResponse> ConfirmBookingAsync(ConfirmBookingRequest request);
    }
}
