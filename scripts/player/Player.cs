using System.Collections.Generic;
using System.Linq;
using Godot;

[GlobalClass]
public partial class Player : Character
{
  [Export]
  public int MyInt;
  InputFrame input = InputFrame.Empty();
  public InputFrame Input => input;
  static HashSet<Player> playerInstances = new HashSet<Player>();
  public static IReadOnlySet<Player> PlayerInstance => playerInstances;
  public static Player? Instance => playerInstances.FirstOrDefault();

  public override void _EnterTree()
  {
    playerInstances.Add(this);
    base._EnterTree();
  }

  public override void _ExitTree()
  {
    playerInstances.Remove(this);
    base._ExitTree();
  }

  public override void _Ready()
  {
    base._Ready();
  }

  public override void _Process(double delta)
  {
    input = InputFrame.Poll(input);
    base._Process(delta);
  }

  public override void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    if (e is DeathEvent de)
    {
      QueueFree();
    }
  }
}
