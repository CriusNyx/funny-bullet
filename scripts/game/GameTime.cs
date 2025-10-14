using Godot;

[GlobalClass]
public partial class GameTime : Node
{
  public static GameTime Instance => Game.Instance.NotNull().gameTime;

  public static double TimeSinceStartup => Instance.timeSinceStartup;

  [Export]
  public double timeSinceStartup { get; private set; } = 0;

  public override void _Process(double delta)
  {
    timeSinceStartup += delta;
    base._Process(delta);
  }
}
