namespace MovieHub.API.Models
{
    public class ConfirmBookingResponse
    {
        public string BookingReference { get; set; }
        public string[] BookedSeats { get; set; }
    }
}
