public class ConcurrentNode : InterpreterNode
{
  public float delayPerItem;

  public ConcurrentNode(string name, InterpreterNode[] children, float delayPerItem = 0)
    : base(name, children)
  {
    this.delayPerItem = delayPerItem;
  }

  protected override void _Start()
  {
    foreach (var (child, index) in Children.WithIndex())
    {
      Push(new DelayNode("Delay", [child], index * delayPerItem));
    }
  }

  protected override LevelInterpreterResult _Update(double deltaTime)
  {
    return LevelInterpreterResult.Done;
  }
}
