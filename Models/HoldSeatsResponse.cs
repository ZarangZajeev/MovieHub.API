namespace MovieHub.API.Models
{
    public class HoldSeatsResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string[] HeldSeats { get; set; }
        public DateTime? HoldExpiry { get; set; }
    }
}
