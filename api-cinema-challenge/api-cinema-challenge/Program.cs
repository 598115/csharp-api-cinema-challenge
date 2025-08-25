using api_cinema_challenge.Data;
using api_cinema_challenge.Endpoints;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CinemaContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.LogTo(message => Debug.WriteLine(message));
});

builder.Services.AddScoped(typeof(IRepository<Customer>), typeof(Repo<Customer>));
builder.Services.AddScoped(typeof(IRepository<Movie>), typeof(Repo<Movie>));
builder.Services.AddScoped(typeof(IRepository<Screening>), typeof(Repo<Screening>));
builder.Services.AddScoped(typeof(IRepository<Ticket>), typeof(Repo<Ticket>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureCinemaEndpoint();
app.Run();
