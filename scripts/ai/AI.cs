using System.Collections.Generic;
using Godot;

public partial class AI : Node3D
{
  [Export]
  public PackedScene bulletPrefab = null!;

  public override void _Ready()
  {
    base._Ready();
  }
}
