using Godot;

[GlobalClass]
public partial class Concurrent : Node, LevelNode
{
  [Export]
  public float delayPerSpawn = 0;

  public InterpreterNode GetNode()
  {
    return new ConcurrentNode(Name, this.GetChildrenNodes(), delayPerSpawn);
  }
}
