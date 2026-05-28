using System.Text;

namespace UrlShortener;

public class Base62Tools {
  public static string ToBase62(int id) {
    var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    var result = new StringBuilder();
  
    while (id > 0) {
      result.Insert(0, chars[id % 62]);
      id /= 62;
    }
  
    return result.ToString();
  }

  public static int FromBase62(string code) {
    var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    var result = 0;

    foreach (var c in code) {
      result = result * 62 + chars.IndexOf(c);
    }

    return result;
  }
}
