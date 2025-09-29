using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class LinqExtensions
{
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

  /// <summary>
  /// Returns the elements of the sequence that are the specified type.
  /// </summary>
  /// <param name="elements"></param>
  /// <typeparam name="U"></typeparam>
  /// <returns></returns>
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

  public static IEnumerable<object> AsTypedEnumerable(this IEnumerable enumerable)
  {
    foreach (var element in enumerable)
    {
      yield return element;
    }
  }

  public static IEnumerable<object> RunConcurrent(this IEnumerable<IEnumerable<object>> elements)
  {
    var enumerators = elements.Select(element => element.GetEnumerator()).ToArray<IEnumerator?>();
    bool done = true;
    while (!done)
    {
      for (int i = 0; i < enumerators.Length; i++)
      {
        if (enumerators[i] is IEnumerator<object> enumerator)
        {
          if (!enumerator.MoveNext())
          {
            enumerators[i] = null;
          }
        }
      }
      done = !enumerators.Any(x => x != null);
      yield return null!;
    }
  }

  public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
  {
    return source.SelectMany(x => x);
  }
}
