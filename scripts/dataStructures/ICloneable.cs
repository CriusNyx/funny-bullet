using System;
using System.Collections.Generic;
using System.Linq;

public interface ICloneable<T>
{
  public T Clone();
}

public static class ICloneableExtensions
{
  public static IEnumerable<T> CloneArr<T>(this IEnumerable<T> source)
    where T : ICloneable<T>
  {
    return source.Select(x => x.Clone());
  }
}
