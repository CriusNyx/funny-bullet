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
}
