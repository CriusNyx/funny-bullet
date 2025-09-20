using Godot;

[GlobalClass]
public partial class DebugEnemy : Character
{
  [Export]
  public double lifetime;
  double timeAlive;

  public override void _Process(double delta)
  {
    if (timeAlive > lifetime)
    {
      Kill();
    }
    timeAlive += delta;
    base._Process(delta);
  }
}
