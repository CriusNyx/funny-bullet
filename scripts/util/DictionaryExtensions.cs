using System;
using System.Collections.Generic;

public static class DictionaryExtensions
{
  public static Value Safe<Key, Value>(this IReadOnlyDictionary<Key, Value> dictionary, Key key)
  {
    if (dictionary.TryGetValue(key, out var value))
    {
      return value;
    }
    return default;
  }

  public static Value Safe<Value>(this Value[] arr, int index)
  {
    if (index >= 0 && index < arr.Length)
    {
      return arr[index];
    }
    return default;
  }

  public static U And<T, U>(this T value, Func<T, U> func)
    where T : class
  {
    if (value != null)
    {
      return func(value);
    }
    return default;
  }
}
