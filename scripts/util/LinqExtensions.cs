using System;
using System.Collections.Generic;
using System.Linq;

public static class LinqExtensions
{
  public static IEnumerable<T> If<T>(
    this IEnumerable<T> source,
    bool condition,
    Func<IEnumerable<T>, IEnumerable<T>> func
  )
  {
    if (condition)
    {
      return func(source);
    }
    return source;
  }

  public static float? SafeMax<T>(this IEnumerable<T> source, Func<T, float> func)
  {
    try
    {
      return source.Max(func);
    }
    catch
    {
      return null;
    }
  }
}
