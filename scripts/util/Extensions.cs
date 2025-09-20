using System;

public static class Extensions
{
  public static Element[] AsArray<Element>(this Element element)
  {
    return [element];
  }

  public static T? As<T>(this object element)
  {
    if (element is T t)
    {
      return t;
    }
    return default;
  }

  public static T Touch<T>(this T value, Action<T> action)
  {
    action(value);
    return value;
  }

  public static (T, U) With<T, U>(this T value, U other)
  {
    return (value, other);
  }
}
