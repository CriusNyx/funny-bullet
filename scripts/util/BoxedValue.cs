public class BoxedValue<T> : IReadonlyBoxedValue<T>
{
  public T Value { get; private set; }

  public BoxedValue(T value = default!)
  {
    Value = value;
  }

  public void Set(T value)
  {
    Value = value;
  }
}

public interface IReadonlyBoxedValue<T>
{
  public T Value { get; }
}
