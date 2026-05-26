namespace UrlShortener.Models;

public class ShortUrl {
  public int Id { get; set; }
  required public string Code { get; set; }
  required public string LongUrl { get; set; }
  public int ClickCount { get; set; }
  public DateTime CreatedAt { get; set; }
}

public record ShortenRequest(string Url);
