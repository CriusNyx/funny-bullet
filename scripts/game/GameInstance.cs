using Godot;

public partial class GameInstance : Node
{
  public static GameInstance Instance { get; private set; }
  public static Scheduler Scheduler { get; private set; }
  public Debugger Debugger { get; private set; }
  public LevelInterpreter LevelInterpreter { get; private set; }
  Camera3D camera;

  public override void _Ready()
  {
    Instance = this;
    base._Ready();
    Scheduler = new Scheduler();
    Debugger = new Debugger() { Name = "Debugger" }.WithParent(this);
    LevelInterpreter = new LevelInterpreter().WithParent(this);

    camera = this.AppendChild(new Camera3D())
      .WithTransform(Vector3.Back * GameStats.CAMERA_DISTANCE);
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    Scheduler.Process();
  }
}
