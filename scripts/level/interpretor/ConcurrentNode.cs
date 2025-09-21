using System.Collections.Generic;

public class ConcurrentNode : InterpreterNode
{
  private Queue<InterpreterNode> queue = new Queue<InterpreterNode>();

  public ConcurrentNode(string name, InterpreterNode[] children)
    : base(name, children) { }

  protected override void _Start()
  {
    foreach (var (child, index) in Children.WithIndex())
    {
      child.Start();
      queue.Enqueue(child);
    }
  }

  protected override LevelInterpreterResult _Update(double deltaTime)
  {
    return RunConcurrent(queue, deltaTime);
  }

  public override IEnumerable<InterpreterNode> LiveChildren()
  {
    return queue;
  }
}
