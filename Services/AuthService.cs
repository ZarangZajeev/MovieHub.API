using MovieHub.API.Data.Factory;
using MovieHub.API.Data.Interfaces;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPostgresDataProvider _dataProvider;

        public AuthService(DataProviderFactory factory)
        {
            _dataProvider = factory.Create();
        }

        public async Task<bool> ValidateGoogleUserAsync(string email, string googleid)
        {
            return await _dataProvider.UserExistsAsync(email, googleid);
        }
    }
}
