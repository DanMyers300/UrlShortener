using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

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

  return Results.Ok();
});

app.Run();
