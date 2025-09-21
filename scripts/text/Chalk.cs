public static class Chalk
{
  const string GREEN = "#22c55e";

  public static string Red(this string source)
  {
    return $"[color=red]{source}[/color]";
  }

  public static string Green(this string source)
  {
    return $"[color={GREEN}]{source}[/color]";
  }

  public static string Gray(this string source)
  {
    return $"[color=gray]{source}[/color]";
  }
}
