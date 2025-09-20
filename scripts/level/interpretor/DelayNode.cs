public class DelayNode : InterpreterNode
{
  public float delayAmount;
  private bool hasStarted = false;

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
    else if (!hasStarted)
    {
      hasStarted = true;
      foreach (var child in Children)
      {
        Push(child);
      }
    }

    return LevelInterpreterResult.Done;
  }
}
