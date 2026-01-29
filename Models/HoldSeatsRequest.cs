namespace MovieHub.API.Models
{
    public class HoldSeatsRequest
    {
        public int ShowId { get; set; }
        public string Username { get; set; }
        public string[] Seats { get; set; }
    }
}
