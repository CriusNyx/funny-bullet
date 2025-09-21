using System.Collections.Generic;
using System.Linq;
using Godot;

public enum LevelInterpreterResult
{
  Done = 0,
  Running = 1,
  Idle = 2,
}

public abstract class InterpreterNode
{
  public string Name { get; private set; }
  public InterpreterNode[] Children;
  public double TimeSinceEnter { get; private set; } = 0;
  public LevelInterpreterResult lastResult = LevelInterpreterResult.Idle;

  protected InterpreterNode(string name, InterpreterNode[] children)
  {
    Name = name;
    Children = children;
  }

  public void Start()
  {
    TimeSinceEnter = 0;
    _Start();
  }

  protected virtual void _Start() { }

  public LevelInterpreterResult Update(double deltaTime)
  {
    lastResult = _Update(deltaTime);
    var selfResult = CombineResults(lastResult);
    TimeSinceEnter += deltaTime;
    return selfResult;
  }

  protected abstract LevelInterpreterResult _Update(double deltaTime);

  public string GetStatus()
  {
    var status = $"{Name} ({lastResult})";
    switch (lastResult)
    {
      case LevelInterpreterResult.Done:
        return status.Green();
      case LevelInterpreterResult.Idle:
        return status.Gray();
      default:
        return status;
    }
  }

  public IEnumerable<InterpreterNode> GetLiveNodes()
  {
    yield return this;
    foreach (var runningChild in Children.SelectMany(child => child.GetLiveNodes()))
    {
      yield return runningChild;
    }
  }

  public virtual IEnumerable<InterpreterNode> LiveChildren()
  {
    return [];
  }

  private string GetIndentString(int indent)
  {
    return Enumerators.Range(indent).Select(x => "    ").StringJoin("");
  }

  public string GetTreeStatus()
  {
    return GetTreeStatus(0);
  }

  private string GetTreeStatus(int indent)
  {
    return $"{GetIndentString(indent)}{GetStatus()}\n{Children.Select(child => child.GetTreeStatus(indent + 1)).StringJoin("")}";
  }

  public static LevelInterpreterResult RunConcurrent(Queue<InterpreterNode> queue, double deltaTime)
  {
    var result = LevelInterpreterResult.Done;
    var values = queue.ToArray();
    queue.Clear();
    foreach (var node in values)
    {
      if (node.Update(deltaTime) != LevelInterpreterResult.Done)
      {
        queue.Enqueue(node);
        result = LevelInterpreterResult.Running;
      }
    }
    return result;
  }

  public static LevelInterpreterResult RunSequential(Queue<InterpreterNode> queue, double deltaTime)
  {
    if (queue.Count == 0)
    {
      return LevelInterpreterResult.Done;
    }

    var node = queue.Peek();
    switch (node.Update(deltaTime))
    {
      case LevelInterpreterResult.Done:
        queue.Dequeue();
        if (queue.TryPeek(out var element))
        {
          element.Start();
        }
        return RunSequential(queue, deltaTime);
      default:
        return LevelInterpreterResult.Running;
    }
  }

  public static LevelInterpreterResult CombineResults(params LevelInterpreterResult[] results)
  {
    return CombineResults(results as IEnumerable<LevelInterpreterResult>);
  }

  public static LevelInterpreterResult CombineResults(IEnumerable<LevelInterpreterResult> results)
  {
    try
    {
      return results.Max();
    }
    catch
    {
      return LevelInterpreterResult.Done;
    }
  }
}
