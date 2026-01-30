namespace MovieHub.API.Models
{
    public class ConfirmBookingResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string? BookingReference { get; set; }
        public string[] BookedSeats { get; set; } = Array.Empty<string>();
    }
}
