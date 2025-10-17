using System;
using System.Collections.Generic;
using System.Linq;

public static class ArrayExtensions
{
  public static bool HasIndex<T>(this IEnumerable<T> arr, int index)
  {
    return index < arr.Count() && index >= 0;
  }
}
