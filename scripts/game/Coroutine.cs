using System.Collections;
using System.Collections.Generic;
using Godot;

public class Coroutine
{
  public ulong nodeId;
  Stack<IEnumerator> stack = new Stack<IEnumerator>();
  public bool completed { get; private set; } = false;
  public static double DeltaTime => CoroutineHost.DeltaTime;

  public Coroutine(ulong nodeId, IEnumerator enumerator)
  {
    this.nodeId = nodeId;
    stack.Push(enumerator);
  }

  public bool Update()
  {
    var result = _Update();
    if (result)
    {
      completed = true;
    }
    return result;
  }

  private bool _Update()
  {
    if (GodotObject.IsInstanceIdValid(nodeId))
    {
      return Step();
    }
    return false;
  }

  private bool Step()
  {
    // If the stack is empty return false because this coroutine has completed.
    if (stack.Count == 0)
    {
      return false;
    }
    // If the stack moves next then the coroutine is still running
    if (stack.Peek().MoveNext())
    {
      // Check the output. If it is coroutine like push it on the stack and start it.
      if (AsCoroutine(stack.Peek().Current) is IEnumerator enumerator)
      {
        stack.Push(enumerator);
        return Step();
      }
    }
    // If we reached the end of the routine at the top of the stack then pop it and run the next routine on the stack.
    else
    {
      stack.Pop();
      return Step();
    }
    // If we reached this the routine is still running, and return true.
    return true;
  }

  private IEnumerator? AsCoroutine(object source)
  {
    if (source is IEnumerable enumerable)
    {
      return enumerable.GetEnumerator();
    }
    if (source is IEnumerator enumerator)
    {
      return enumerator;
    }
    if (source is ICoroutineOutput coroutineOutput)
    {
      return coroutineOutput.GetRoutine().GetEnumerator();
    }
    return null;
  }
}

public interface ICoroutineOutput
{
  public IEnumerable GetRoutine();
}
