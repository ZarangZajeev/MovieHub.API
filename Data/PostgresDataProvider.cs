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

                var result = (await connection.QueryAsync<(int Status, string HeldSeat, DateTime? HoldExpiry)>("SELECT * FROM hold_seats(@showId, @username, @seats);", new { showId, username, seats } )).ToList();

                var status = result.First().Status;

                if (status == 2)
                {
                    return new HoldSeatsResponse
                    {
                        Status = 2,
                        Message = "One or more seats are already held or booked by another user",
                        HeldSeats = Array.Empty<string>(),
                        HoldExpiry = null
                    };
                }

                return new HoldSeatsResponse
                {
                    Status = 1,
                    Message = "Seats held successfully",
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

                var result = (await connection.QueryAsync<(int Status, string Message, string BookingReference, string BookedSeat)>("SELECT * FROM confirm_booking(@showId, @username, @seats);", new { showId, username, seats })).ToList();

                if (!result.Any() || result.First().Status == 2)
                {
                    return new ConfirmBookingResponse
                    {
                        Status = 2,
                        Message = result.FirstOrDefault().Message
                            ?? "Please hold seats before confirming"
                    };
                }

                return new ConfirmBookingResponse
                {
                    Status = 1,
                    Message = result.First().Message,
                    BookingReference = result.First().BookingReference,
                    BookedSeats = result
                        .Where(r => r.BookedSeat != null)
                        .Select(r => r.BookedSeat)
                        .ToArray()
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

        public async Task<BookingDetailsDto> GetBookingDetailsAsync(string username, string bookingReference)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);

                var result = await connection.QueryFirstOrDefaultAsync<BookingDetailsDto>(
                    "SELECT * FROM get_booking_details(@Username, @BookingReference);",
                    new { Username = username, BookingReference = bookingReference }
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve booking details for reference {bookingReference}", ex);
            }
        }

        public async Task<IEnumerable<UserBookingDto>> GetUserBookingsAsync(string username)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);

                var result = await connection.QueryAsync<UserBookingDto>(
                    "SELECT * FROM get_user_bookings(@Username);",
                    new { Username = username }
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve bookings for user {username}", ex);
            }
        }
    }
}
