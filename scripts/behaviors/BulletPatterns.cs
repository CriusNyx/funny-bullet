using System.Collections.Generic;
using System.Linq;
using Godot;

[GlobalClass]
public partial class BulletPatterns : Behavior
{
  [Export]
  public PackedScene? bullet;

  public Dictionary<string, BulletSpawn[]> patternsCache = new Dictionary<string, BulletSpawn[]>();

  public override void _Ready()
  {
    foreach (var child in this.GetChildrenOfType<BulletPattern>())
    {
      patternsCache.Add(child.Name, child.ProcessSelf().OrderBy(x => x.spawnTime).ToArray());
    }
  }

  public override void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    if (e is FireEvent fireEvent)
    {
      if (patternsCache.TryGetValue(fireEvent.fireParameters?.firePattern ?? "", out var pattern))
      {
        if (Host is BehaviorHost host)
        {
          BulletSpawner.Create(
            pattern,
            new SpawnParameters { },
            host,
            host.Position,
            (fireEvent.fireParameters?.atPlayer ?? false) ? Player.Instance : null
          );
        }
      }
    }
  }
}
