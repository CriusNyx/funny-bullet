using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public interface DebugPrint
{
  IEnumerable<(string, object)> EnumerateFields();
}

public static class DebugPrintExtensions
{
  public static string Debug(this object o)
  {
    if (o is IEnumerable enumerable)
    {
      return $"[\n{enumerable.Cast<object>().Select(Debug).StringJoin().Indent("  ")}\n]";
    }
    if (o is DebugPrint debug)
    {
      return $"{o.GetType().Name} {{\n{debug.EnumerateFields().Select(PrintField).StringJoin("\n").Indent("  ")}\n}}";
    }
    return o.ToString();
  }

  private static string PrintField((string, object) field)
  {
    var (name, value) = field;
    return $"{name}: {Debug(value)}";
  }
}
