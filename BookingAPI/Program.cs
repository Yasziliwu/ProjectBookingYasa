using BookingAPI.Contracts;
using BookingAPI.Controllers;
using BookingAPI.Data;
using BookingAPI.Repositories;
using BookingAPI.Repository;
using BookingAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
builder.Services.AddScoped<IPelangganRepository, PelangganRepository>();
builder.Services.AddScoped<IPemesananKaraokeRepository, BookingKaraokeRepository>();

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
