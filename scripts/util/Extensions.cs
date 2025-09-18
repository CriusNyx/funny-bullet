public static class Extensions
{
  public static Element[] ToArray<Element>(this Element element)
  {
    return [element];
  }

  public static T As<T>(this object element)
  {
    if (element is T t)
    {
      return t;
    }
    return default;
  }
}
