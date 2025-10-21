using System;

public class MutexValue<T>
{
  private object valueLock = new object();
  private T value;

  public T Value
  {
    get
    {
      lock (valueLock)
      {
        return value;
      }
    }
    set
    {
      lock (valueLock)
      {
        this.value = value;
      }
    }
  }

  public MutexValue(T value = default!)
  {
    this.value = value;
  }

  public void Use(Action<T> action)
  {
    lock (valueLock)
    {
      action(value);
    }
  }
}
