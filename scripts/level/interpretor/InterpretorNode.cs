using System.Collections.Generic;
using System.Linq;

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
  private Queue<InterpreterNode> runningChildren = new Queue<InterpreterNode>();
  public LevelInterpreterResult lastResult = LevelInterpreterResult.Idle;

  protected InterpreterNode(string name, InterpreterNode[] children)
  {
    this.Name = name;
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
    var selfResult = CombineResults(lastResult, UpdateChildren(deltaTime));
    TimeSinceEnter += deltaTime;
    return selfResult;
  }

  protected abstract LevelInterpreterResult _Update(double deltaTime);

  public void Push(InterpreterNode node)
  {
    node.Start();
    runningChildren.Enqueue(node);
  }

  public LevelInterpreterResult UpdateChildren(double deltaTime)
  {
    runningChildren.ProcessQueue((child) => child.Update(deltaTime) == LevelInterpreterResult.Done);
    return runningChildren.Count == 0
      ? LevelInterpreterResult.Done
      : LevelInterpreterResult.Running;
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

  public string GetStatus()
  {
    return $"{this.Name} ({lastResult})";
  }

  public IEnumerable<InterpreterNode> GetLiveNodes()
  {
    yield return this;
    foreach (var runningChild in LiveChildren().SelectMany(child => child.GetLiveNodes()))
    {
      yield return runningChild;
    }
  }

  public virtual IEnumerable<InterpreterNode> LiveChildren()
  {
    return runningChildren;
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
    return $"{GetIndentString(indent)}{GetStatus()}\n{LiveChildren().Select(child => child.GetTreeStatus(indent + 1)).StringJoin("")}";
  }
}
