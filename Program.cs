using Microsoft.EntityFrameworkCore;
using UrlShortener;
using UrlShortener.Data;
using UrlShortener.Models;


// --- SETUP --- //


var builder = WebApplication.CreateBuilder(args);

// Needed for docker connections
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=urlshortener.db";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

// Setup front end
app.UseDefaultFiles();
app.UseStaticFiles();


// --- METHODS --- //


app.MapPost("/shorten", async (ShortenRequest req, AppDbContext db, HttpContext ctx) => {
  var url = req.Url;

  Uri.TryCreate(url, UriKind.Absolute, out var uri);

  if (uri == null) {
    return Results.BadRequest("Not a valid URI");
  }

  if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps) {
    return Results.BadRequest("Must be http/https link");
  }

  var shortUrl = new ShortUrl { LongUrl = url, CreatedAt = DateTime.UtcNow };

  db.ShortUrls.Add(shortUrl);

  await db.SaveChangesAsync();

  // Encode the Id but ensure at least 6 characters
  var code = Base62Tools.ToBase62(shortUrl.Id * 56_800_235);

  var baseUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}";

  return Results.Created($"/{code}", new { code, shortUrl = $"{baseUrl}/{code}" });

});


app.MapGet("/{code}", async (string code, AppDbContext db) => {
  var id = Base62Tools.FromBase62(code) / 56_800_235;

  var url = await db.ShortUrls.FindAsync(id);

  if (url == null) return Results.NotFound("Short URL not found");

  return Results.Redirect(url.LongUrl);

});


// --- RUN --- //


using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
