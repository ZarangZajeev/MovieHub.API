using Dapper;
using MovieHub.API.Data.Interfaces;
using MovieHub.API.Models;
using Npgsql;

namespace MovieHub.API.Data
{
    public class PostgresDataProvider : IPostgresDataProvider
    {
        private readonly string _connectionString;

        public PostgresDataProvider(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("PostgresDb");
        }

        public async Task<IEnumerable<ShowDetailsDto>> GetActiveShowsAsync()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);

                return await connection.QueryAsync<ShowDetailsDto>(
                    "SELECT * FROM get_active_shows();"
                );
            }
            catch (Exception ex)
            {
                // Preserve stack trace
                throw new Exception($"Failed to retrieve Active Shows ", ex);
            }
        }

        public async Task<SpecificShowDetails> GetShowSeatDetailsAsync(int showId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);

                return await connection.QuerySingleOrDefaultAsync<SpecificShowDetails>(
                    "SELECT * FROM get_show_seat_details(@showId);",
                    new { showId }
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve seat details for show ID {showId}", ex);
            }
        }

        public async Task<HoldSeatsResponse> HoldSeatsAsync(int showId, string username, string[] seats)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);

                var result = await connection.QueryAsync<(string HeldSeat, DateTime HoldExpiry)>(
                    "SELECT * FROM hold_seats(@showId, @username, @seats);",
                    new { showId, username, seats }
                );

                return new HoldSeatsResponse
                {
                    HeldSeats = result.Select(r => r.HeldSeat).ToArray(),
                    HoldExpiry = result.First().HoldExpiry
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to hold seats for show ID {showId}", ex);
            }
        }

        public async Task<ConfirmBookingResponse> ConfirmBookingAsync(int showId, string username, string[] seats)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);

                var result = await connection.QueryAsync<(string BookingReference, string BookedSeat)>(
                    "SELECT * FROM confirm_booking(@showId, @username, @seats);",
                    new { showId, username, seats }
                );

                return new ConfirmBookingResponse
                {
                    BookingReference = result.First().BookingReference,
                    BookedSeats = result.Select(r => r.BookedSeat).ToArray()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to confirm booking for show ID {showId}", ex);
            }
        }


        public async Task ReleaseExpiredHoldsAsync()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.ExecuteAsync("SELECT release_expired_holds();");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to release expired holds", ex);
            }
        }
    }
}
