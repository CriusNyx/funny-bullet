using System;
using System.Collections;
using System.Collections.Generic;

public static class Enumerators
{
  public static IEnumerable<float> Range(int count)
  {
    for (int i = 0; i < count; i++)
    {
      yield return i;
    }
  }

  public static IEnumerable<float> Step(float min, float max, int count)
  {
    float range = max - min;
    for (int i = 0; i < count; i++)
    {
      yield return min + range * ((float)i / count - 1);
    }
  }

  public static IEnumerable ToEnumerable(this Action action)
  {
    action();
    yield return null;
  }
}
