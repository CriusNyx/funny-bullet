using Godot;

[GlobalClass]
public partial class Delay : Node, LevelNode
{
  [Export]
  public float delay;

  public InterpreterNode GetNode()
  {
    return new DelayNode(Name, this.GetChildrenNodes(), delay);
  }
}
