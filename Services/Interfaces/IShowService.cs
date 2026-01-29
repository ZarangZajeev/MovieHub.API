using MovieHub.API.Models;

namespace MovieHub.API.Services.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<ShowDetailsDto>> GetActiveShowsAsync();
        Task<SpecificShowDetails> GetShowDetailsAsync(int showId);
    }

}
