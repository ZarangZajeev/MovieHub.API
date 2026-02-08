namespace MovieHub.API.Models.Auth
{
    public class GoogleLoginRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string GoogleId { get; set; }
    }
}
