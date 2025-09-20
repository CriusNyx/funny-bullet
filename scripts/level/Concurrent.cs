using Godot;

[GlobalClass]
public partial class Concurrent : Node, LevelNode
{
  [Export]
  public float spawnDelay = 0;

  public InterpreterNode GetNode()
  {
    return new ConcurrentNode(Name, this.GetChildrenNodes(), spawnDelay);
  }
}
