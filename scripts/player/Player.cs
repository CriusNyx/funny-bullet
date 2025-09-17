using Godot;
using static GameStats;

[GlobalClass]
public partial class Player : Node3D
{
  InputFrame input;

  public override void _Process(double delta)
  {
    base._Process(delta);
    input = InputFrame.Poll(input);

    Position += input.Current.GetInputVector().To3() * (float)delta * PLAYER_SPEED;
  }
}
