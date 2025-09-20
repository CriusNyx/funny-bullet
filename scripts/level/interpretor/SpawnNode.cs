using System.Security.Cryptography.X509Certificates;
using Godot;

public class SpawnNode : InterpreterNode
{
  PackedScene? prefab;
  bool finished = false;

  public SpawnNode(string name, InterpreterNode[] children, PackedScene? prefab = null)
    : base(name, children)
  {
    this.prefab = prefab;
  }

  protected override void _Start()
  {
    var instance = prefab
      ?.Instantiate()
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
