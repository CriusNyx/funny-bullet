using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// A behavior to hold bullet patterns to spawn for the NPC.
/// </summary>
[GlobalClass]
public partial class BulletPatterns : Node, Behavior
{
  public Dictionary<string, BulletSpawn[]> patternsCache = new Dictionary<string, BulletSpawn[]>();

  public override void _Ready()
  {
    foreach (var child in this.GetChildrenOfType<BulletPattern>())
    {
      patternsCache.Add(child.Name, child.ProcessSelf().OrderBy(x => x.spawnTime).ToArray());
    }
  }

  public void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    if (e is FireEvent fireEvent)
    {
      if (patternsCache.TryGetValue(fireEvent.fireParameters?.firePattern ?? "", out var pattern))
      {
        if (this.GetActor() is Actor host)
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
