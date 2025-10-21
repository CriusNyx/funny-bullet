using System.Collections.Generic;

public class ArrayStream<T> : ICloneable<ArrayStream<T>>
{
  public IReadOnlyList<T> array;
  public int Index { get; set; }

  public ArrayStream(IReadOnlyList<T> array, int index = 0)
  {
    this.array = array;
    Index = index;
  }

  public ArrayStream<T> Clone()
  {
    return new ArrayStream<T>(array, Index);
  }

  public bool TryRead(out T t)
  {
    if (!array.HasIndex(Index))
    {
      t = default!;
      return false;
    }
    else
    {
      t = array[Index++];
      return true;
    }
  }

  public T? SafeRead()
  {
    return array.Safe(Index++);
  }

  public T Read()
  {
    return array[Index++];
  }

  public T Peek()
  {
    return array[Index];
  }
}
