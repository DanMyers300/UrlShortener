using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=urlshortener.db"));

var app = builder.Build();

app.MapPost("/shorten", (ShortenRequest req, AppDbContext db) => {

});

app.Run();
