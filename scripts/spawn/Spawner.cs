using Godot;

[GlobalClass]
public partial class Spawner : Node3D
{
  [Export]
  PackedScene? prefab;

  public void Spawn()
  {
    if (prefab != null)
    {
      var instance = prefab
        .Instantiate()
        .WithParent(GameInstance.Instance.enemies)
        .Touch(x => x.As<Node3D>()?.WithTransform(Position));
      if (this.GetParentOfType<Wave>() is Wave wave)
      {
        var tracker = new Tracker();
        wave.TrackOne();
        instance.WithOnDeath((x) => wave.UntrackOne());
      }
    }
  }
}
