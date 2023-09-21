using Microsoft.EntityFrameworkCore;
using SenaHotelBookings.Dal;
using SenaHotelBookings.Dal.Repositories;
using SenaHotelBookings.Domain.Contracts.Repositories;
using SenaHotelBookings.Domain.Contracts.Services;
using SenaHotelBookings.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultCon"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHotelRepositories, HotelRepositories>();
builder.Services.AddScoped<IReservationService, ReservationService>();
//builder.Services.AddAutoMapper(typeof(Startup));

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
