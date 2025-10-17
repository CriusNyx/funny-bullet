using System;

public static class Functional
{
  public static bool Try<T>(Func<T> function, out T result)
  {
    try
    {
      result = function();
      return true;
    }
    catch
    {
      result = default!;
      return false;
    }
  }

  public static T? Safe<T>(Func<T> function)
  {
    try
    {
      return function();
    }
    catch
    {
      return default!;
    }
  }
}
