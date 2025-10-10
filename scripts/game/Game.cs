using Godot;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public partial class Game : Node
{
  public static Game? Instance { get; private set; }
  public static Scheduler Scheduler { get; private set; }
  public Debugger Debugger { get; private set; }
  public CoroutineHost CoroutineHost { get; private set; }
  public SafeSpace SafeSpace { get; private set; }
  public Node enemies { get; private set; }

  GameCamera camera;

  public override void _Ready()
  {
    Instance = this;
    base._Ready();
    Scheduler = new Scheduler();
    Debugger = new Debugger() { Name = "Debugger" }.WithParent(this);

    camera = new GameCamera().WithName("Camera").WithParent(this);

    CoroutineHost = new CoroutineHost().WithParent(this);

    SafeSpace = new SafeSpace().WithParent(this);

    enemies = new Node().WithName("Enemies").WithParent(this);
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    Scheduler.Process();
  }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
