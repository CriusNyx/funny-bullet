using Godot;

[GlobalClass]
public partial class Spawn : Node, LevelNode
{
  [Export]
  public PackedScene? prefab;

  [Export]
  public SpawnInfo spawnInfo = new SpawnInfo();

  public InterpreterNode GetNode()
  {
    return new SpawnNode(Name, this.GetChildrenNodes(), this);
  }
}

public class SpawnNode : InterpreterNode
{
  Spawn spawn;
  bool finished = false;

  public SpawnNode(string name, InterpreterNode[] children, Spawn spawn)
    : base(name, children)
  {
    this.spawn = spawn;
  }

  protected override void _Start()
  {
    var instance = spawn
      .prefab?.Instantiate()
      .WithParent(GameInstance.Instance)
      .As<Character>()
      ?.OnDeath(
        (character) =>
        {
          finished = true;
        }
      );
    finished = instance == null;
  }

  protected override LevelInterpreterResult _Update(double deltaTime)
  {
    return finished ? LevelInterpreterResult.Done : LevelInterpreterResult.Running;
  }
}
