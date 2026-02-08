namespace MovieHub.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateGoogleUserAsync(string email, string googleid);
    }
}
