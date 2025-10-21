using System;
using System.Collections;
using Godot;

public static class Extensions
{
  public static Element[] AsArray<Element>(this Element element)
  {
    return [element];
  }

  public static T? TouchIfNull<T>(this T? element, Action action)
    where T : class
  {
    if (element is null)
    {
      action();
    }
    return element;
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

  public static T Tap<T>(this T value)
  {
    return value.Touch(x => GD.Print(value));
  }

  public static T TapD<T>(this T value)
  {
    return value.Touch(x => GD.Print(value?.Debug()));
  }

  public static (T, U) With<T, U>(this T value, U other)
  {
    return (value, other);
  }

  public static (T, U, V) AndWith<T, U, V>(this (T, U) value, V other)
  {
    return (value.Item1, value.Item2, other);
  }

  public static (T, U, V, W) AndWith<T, U, V, W>(this (T, U, V) value, W other)
  {
    return (value.Item1, value.Item2, value.Item3, other);
  }

  public static (T, U, V, W, X) AndWith<T, U, V, W, X>(this (T, U, V, W) value, X other)
  {
    return (value.Item1, value.Item2, value.Item3, value.Item4, other);
  }

  public static U Transform<T, U>(this T value, Func<T, U> transformationFunc)
  {
    return transformationFunc(value);
  }

  public static T TransformIf<T>(this T source, bool condition, Func<T, T> func)
  {
    if (condition)
    {
      return func(source);
    }
    return source;
  }

  public static T? WarnNull<T>(this T? value)
    where T : class
  {
    if (value == null)
    {
      GD.PushWarning("Expected value to not be null");
    }
    return value;
  }

  public static T? WarnIfNotNull<T>(this T? value)
    where T : class
  {
    if (value != null)
    {
      GD.PushWarning(
        $"Expected value {value} to be null, but is is {value.GetType().Name} instead."
      );
    }
    return value;
  }

  public static T NotNull<T>(this T? value, string fieldName = null)
    where T : class
  {
    if (value is null)
    {
      throw new NullReferenceException($"Vaule {fieldName} was expected to not be null");
    }
    return value;
  }
}
