namespace MovieHub.API.Models
{
    public class HoldSeatsResponse
    {
        public string[] HeldSeats { get; set; }
        public DateTime HoldExpiry { get; set; }
    }
}
