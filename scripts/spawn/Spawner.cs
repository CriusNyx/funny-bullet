using Godot;

[GlobalClass]
public partial class Spawner : Node3D
{
  [Export]
  PackedScene? prefab;

  [Export]
  PackedScene[] behaviors = [];

  public void Spawn()
  {
    if (prefab != null)
    {
      var instance = prefab
        .Instantiate()
        .WithParent(Game.Instance.enemies)
        .Touch(x => x.As<Node3D>()?.WithTransform(Position));
      if (this.GetParentOfType<Wave>() is Wave wave)
      {
        var tracker = new Tracker();
        wave.TrackOne();
        instance.WithOnDeath((x) => wave.UntrackOne());
      }

      if (instance != null)
      {
        foreach (var behavior in behaviors)
        {
          behavior.Instantiate().WithParent(instance);
        }
      }
    }
  }
}
