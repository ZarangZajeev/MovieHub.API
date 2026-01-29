namespace MovieHub.API.Models
{
    public class ConfirmBookingRequest
    {
        public int ShowId { get; set; }
        public string Username { get; set; }
        public string[] Seats { get; set; }
    }
}
