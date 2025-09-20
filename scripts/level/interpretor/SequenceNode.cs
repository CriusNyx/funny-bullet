using System.Collections.Generic;
using System.Linq;

public class SequenceNode : InterpreterNode, DebugPrint
{
  int currentChildIndex = 0;
  float delayPerItem;
  InterpreterNode? Current;

  public SequenceNode(string name, InterpreterNode[] children, float delayPerItem = 0)
    : base(name, children)
  {
    this.delayPerItem = delayPerItem;
  }

  protected override void _Start()
  {
    Current = Children.FirstOrDefault();
    Current?.Start();
    base._Start();
  }

  protected override LevelInterpreterResult _Update(double deltaTime)
  {
    if (Current == null)
    {
      return LevelInterpreterResult.Done;
    }
    var result = Current.Update(deltaTime);
    if (result == LevelInterpreterResult.Running)
    {
      return LevelInterpreterResult.Running;
    }
    else
    {
      currentChildIndex++;
      Current = Children
        .Safe(currentChildIndex)
        .And((node) => delayPerItem != 0 ? new DelayNode("Delay", [node], delayPerItem) : node);
      Current?.Start();
      return _Update(deltaTime);
    }
  }

  public override IEnumerable<InterpreterNode> LiveChildren()
  {
    if (Current != null)
    {
      yield return Current;
    }
  }

  public IEnumerable<(string, object)> EnumerateFields()
  {
    return ["children".With(Children)];
  }
}
