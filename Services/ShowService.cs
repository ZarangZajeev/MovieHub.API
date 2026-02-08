using MovieHub.API.Data.Factory;
using MovieHub.API.Data.Interfaces;
using MovieHub.API.Models;
using MovieHub.API.Services.Interfaces;

namespace MovieHub.API.Services
{
    public class ShowService : IShowService
    {
        private readonly IPostgresDataProvider _dataProvider;

        public ShowService(DataProviderFactory factory)
        {
            _dataProvider = factory.Create();
        }

        public Task<IEnumerable<ShowDetailsDto>> GetActiveShowsAsync()
        {
            return _dataProvider.GetActiveShowsAsync();
        }

        public Task<SpecificShowDetails> GetShowDetailsAsync(int showId)
        {
            return _dataProvider.GetShowSeatDetailsAsync(showId);
        }
    }

}
