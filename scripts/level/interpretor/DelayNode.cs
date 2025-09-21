using System.Collections.Generic;

public class DelayNode : InterpreterNode
{
  public float delayAmount;
  private bool hasStarted = false;
  Queue<InterpreterNode> queue = new Queue<InterpreterNode>();

  public DelayNode(string name, InterpreterNode[] children, float delayAmount = 0)
    : base(name, children)
  {
    this.delayAmount = delayAmount;
  }

  protected override void _Start()
  {
    hasStarted = false;
  }

  protected override LevelInterpreterResult _Update(double deltaTime)
  {
    if (TimeSinceEnter < delayAmount)
    {
      return LevelInterpreterResult.Running;
    }

    if (!hasStarted)
    {
      hasStarted = true;
      foreach (var child in Children)
      {
        child.Start();
        queue.Enqueue(child);
      }
    }

    return RunConcurrent(queue, deltaTime);
  }

  public override IEnumerable<InterpreterNode> LiveChildren()
  {
    return queue;
  }
}
