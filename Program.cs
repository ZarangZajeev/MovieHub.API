using MovieHub.API.Data;
using MovieHub.API.Data.Interfaces;
using MovieHub.API.Services;
using MovieHub.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IPostgresDataProvider, PostgresDataProvider>();
builder.Services.AddHostedService<SeatHoldCleanupService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Empty);
app.Run();
