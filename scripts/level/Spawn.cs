using Godot;

[GlobalClass]
public partial class Spawn : Node, LevelNode
{
  [Export]
  public PackedScene? prefab;

  public InterpreterNode GetNode()
  {
    return new SpawnNode(this.Name, this.GetChildrenNodes(), prefab);
  }
}
