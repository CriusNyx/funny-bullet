using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

  public static IEnumerable<(T, int)> WithIndex<T>(this IEnumerable<T> values)
  {
    int index = 0;
    foreach (var value in values)
    {
      yield return (value, index);
      index++;
    }
  }

  public static int ProcessQueue<T>(this Queue<T> queue, Func<T, bool> processor)
  {
    var len = queue.Count;
    int output = 0;
    for (int i = 0; i < len; i++)
    {
      var val = queue.Dequeue();
      if (!processor(val))
      {
        queue.Enqueue(val);
      }
    }
    return output;
  }

  public static IEnumerable<U> WhereAs<U>(this IEnumerable<object> elements)
  {
    return elements.Where(x => x is U).Select(x => x!.As<U>()) as IEnumerable<U>;
  }

  public static IEnumerable<T> WhereDefined<T>(this IEnumerable<T?> values)
  {
    return values.Where(x => x != null) as IEnumerable<T>;
  }

  public static void Foreach<T>(this IEnumerable<T> values, Action<T> action)
  {
    foreach (var value in values)
    {
      action(value);
    }
  }
}
