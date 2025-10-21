using System;

public class Thunk<T>
{
  bool hasGenerated = false;
  T value = default!;
  Func<T> generator;

  public T Value
  {
    get
    {
      if (!hasGenerated)
      {
        hasGenerated = true;
        value = generator();
      }
      return value;
    }
  }

  public Thunk(Func<T> generator)
  {
    this.generator = generator;
  }
}
