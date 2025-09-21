using System.Linq;
using Godot;

[GlobalClass]
public partial class Concurrent : Node, LevelNode
{
  [Export]
  public float delayPerSpawn = 0;

  public InterpreterNode GetNode()
  {
    return new ConcurrentNode(
      Name,
      this.GetChildrenNodes()
        .Select(
          (node, index) =>
          {
            float delay = index * delayPerSpawn;
            return delay == 0 ? node : new DelayNode("Delay", [node], delay);
          }
        )
        .ToArray()
    );
  }
}
