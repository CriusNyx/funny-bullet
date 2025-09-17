using System.Collections.Generic;

public static class StringExtensions
{
  public static string Indent(this string source, string indentation)
  {
    return $"{indentation}{source.Replace("\n", "\n" + indentation)}";
  }

  public static string StringJoin(this IEnumerable<string> source, string separator = "\n")
  {
    return string.Join(separator, source);
  }
}
