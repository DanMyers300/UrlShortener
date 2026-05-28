using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=urlshortener.db"));

var app = builder.Build();

app.MapPost("/shorten", async (ShortenRequest req, AppDbContext db) => {
  var url = req.Url;

  if (string.IsNullOrWhiteSpace(url)) {
    return Results.BadRequest("Must provide a URL");
  }

  Uri.TryCreate(url, UriKind.Absolute, out var uri);

  if (uri == null) {
    return Results.BadRequest("Not a valid URI");
  }

  if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps) {
    return Results.BadRequest("Must be http/https link");
  }

  bool exists = await db.ShortUrls.FirstOrDefaultAsync(s => s.LongUrl == url);

  if (exists) {
    // To-Do: Return existing short code
  }

  var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

  for (int i = 0; i < 5; i++) {
    var index = RandomNumberGenerator.GetInt32(0, allowedChars.Length);
    // To-Do: Finish building short url string
  }

  return Results.Ok();
});

app.Run();
