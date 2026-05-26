namespace UrlShortener;

public class ShortUrl {
  public int Id { get; set; }
  required public string Code { get; set; }
  required public string LongUrl { get; set; }
  public int ClickCount { get; set; }
  public DateTime CreatedAt { get; set; }
}

class Program {
  static void Main(string[] args) {
    
  }
}
