namespace MovieHub.API.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username);
    }
}
