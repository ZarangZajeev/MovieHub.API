using MovieHub.API.Data.Interfaces;

namespace MovieHub.API.Services
{
    public class SeatHoldCleanupService : BackgroundService
    {
        private readonly ILogger<SeatHoldCleanupService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public SeatHoldCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<SeatHoldCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SeatHoldCleanupService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Create a new scope
                    using var scope = _scopeFactory.CreateScope();
                    var dataProvider = scope.ServiceProvider.GetRequiredService<IPostgresDataProvider>();

                    // Call the PostgreSQL function to release expired holds
                    await dataProvider.ReleaseExpiredHoldsAsync();

                    _logger.LogInformation("Released expired seat holds at {Time}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error releasing expired seat holds");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("SeatHoldCleanupService stopped.");
        }
    }
}
