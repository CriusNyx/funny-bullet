using System.Collections.Generic;

public class CircleQueue<T>
  where T : class
{
  public int MaxCount { get; private set; }
  public int Count => inner.Count;
  private Queue<T> inner = new Queue<T>();

  public CircleQueue(int maxCount)
  {
    MaxCount = maxCount;
  }

  public void Enqueue(T element)
  {
    inner.Enqueue(element);
    while (inner.Count > MaxCount)
    {
      inner.Dequeue();
    }
  }

  public T Dequeue()
  {
    return inner.Dequeue();
  }

  public bool TryDequeue(out T element)
  {
    return inner.TryDequeue(out element);
  }

  public T Peek()
  {
    return inner.Peek();
  }

  public bool TryPeek(out T element)
  {
    return inner.TryPeek(out element);
  }

  public bool Contains(T element)
  {
    return inner.Contains(element);
  }
}
