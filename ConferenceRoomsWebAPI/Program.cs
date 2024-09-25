using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.CachedRepositories;
using ConferenceRoomsWebAPI.Interfaces;
using ConferenceRoomsWebAPI.Repositories;
using ConferenceRoomsWebAPI.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<IBookingRepository, CachedBookingRepository>();
        builder.Services.AddScoped<BookingRepository>();
        builder.Services.AddScoped<ICompanyConferenceServiceRepository, CachedCompanyConferenceServiceRepository>();
        builder.Services.AddScoped<CompanyConferenceServiceRepository>();
        builder.Services.AddScoped<IConferenceRoomRepository, CachedConferenceRoomRepository>();
        builder.Services.AddScoped<ConferenceRoomRepository>();
        builder.Services.AddScoped<IBookingService, BookingSerivce>();
        builder.Services.AddScoped<ICompanyConferenceService, CompanyConferenceService>();
        builder.Services.AddScoped<IConferenceRoomService, ConferenceRoomService>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext"));
        });

        builder.Services.AddMemoryCache();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}