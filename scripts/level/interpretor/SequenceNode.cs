using System.Collections.Generic;

public class SequenceNode : InterpreterNode, DebugPrint
{
  float delayPerItem;
  Queue<InterpreterNode> queue = new Queue<InterpreterNode>();

  public SequenceNode(string name, InterpreterNode[] children, float delayPerItem = 0)
    : base(name, children)
  {
    this.delayPerItem = delayPerItem;
  }

  protected override void _Start()
  {
    foreach (var (child, index) in Children.WithIndex())
    {
      float delay = delayPerItem * index;
      queue.Enqueue(delay == 0 ? child : new DelayNode("Delay", [child], delayPerItem));
    }
    if (queue.TryPeek(out var first))
    {
      first.Start();
    }
    base._Start();
  }

  protected override LevelInterpreterResult _Update(double deltaTime)
  {
    return RunSequential(queue, deltaTime);
  }

  public override IEnumerable<InterpreterNode> LiveChildren()
  {
    return queue;
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return ["children".With(Children)];
  }
}
