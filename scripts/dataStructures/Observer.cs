using System;
using System.Reflection.Metadata;

public class Observable<T>
{
  private T value;
  private Func<T, T, bool> comparator;

  public event Action<T, T>? OnChange;
  public T Value
  {
    get => value;
    set
    {
      if (!comparator(value, this.value))
      {
        var previous = this.value;
        this.value = value;
        HandleChange(previous);
      }
    }
  }

  public Observable(
    T value = default!,
    Func<T, T, bool>? comparator = null,
    Action<T, T>? OnChange = null
  )
  {
    this.value = value;
    this.comparator = comparator ?? ((a, b) => Equals(a, b));
    if (OnChange != null)
    {
      this.OnChange += OnChange;
    }
  }

  private void HandleChange(T previous)
  {
    OnChange?.Invoke(value, previous);
  }
}

public class Transformer<T, U> : IDisposable
{
  Observable<T> source;
  Func<T, U> transformationFunc;
  Observable<U> self;

  public event Action<U, U>? OnChange;

  public Transformer(
    Observable<T> observable,
    Func<T, U> transformationFunc,
    Func<U, U, bool> comparator
  )
  {
    this.transformationFunc = transformationFunc;
    this.source = observable;
    self = new Observable<U>(transformationFunc(source.Value), comparator);
    self.OnChange += HandleChange;

    source.OnChange += HandleSourceChange;
  }

  private void HandleChange(U value, U previous)
  {
    this.OnChange?.Invoke(value, previous);
  }

  private void HandleSourceChange(T value, T previous)
  {
    self.Value = transformationFunc(value);
  }

  public void Dispose()
  {
    source.OnChange -= HandleSourceChange;
  }
}
