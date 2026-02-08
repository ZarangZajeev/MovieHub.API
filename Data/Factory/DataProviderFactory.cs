using MovieHub.API.Data.Interfaces;

namespace MovieHub.API.Data.Factory
{
    public class DataProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DataProviderFactory(IServiceProvider serviceProvider,
                                   IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public IPostgresDataProvider Create()
        {
            var provider = _configuration["DatabaseSettings:Provider"];

            return provider switch
            {
                "Postgres" => _serviceProvider.GetRequiredService<PostgresDataProvider>(),
                "Sql" => _serviceProvider.GetRequiredService<SqlDataProvider>(),
                _ => throw new Exception("Invalid DB provider")
            };
        }
    }
}
