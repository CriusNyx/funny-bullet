using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using static GameStats;

[GlobalClass]
public partial class Player : Character
{
  [Export]
  public int MyInt;
  InputFrame input = InputFrame.Empty();
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
    base._Process(delta);
    input = InputFrame.Poll(input);

    Position += input.Current.GetInputVector().To3() * (float)delta * PLAYER_SPEED;
  }

  public override void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    if (e is DeathEvent de)
    {
      QueueFree();
    }
  }
}
