using System.IO;
using Godot;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public partial class Game : Node
{
  public const string GAME_FILES_DIR = "res://game/";

  public static Game? Instance { get; private set; }
  public InputPoller input { get; private set; }
  public GameTime gameTime { get; private set; }
  public static Scheduler Scheduler { get; private set; }
  public Debugger Debugger { get; private set; }
  public SafeSpace SafeSpace { get; private set; }
  public Node enemies { get; private set; }
  public GameUI gameUI { get; private set; }

  GameCamera camera;

  public override void _Ready()
  {
    gameTime = new GameTime().WithName("GameTime").WithParent(this);

    InitializeSubComponents();

    input = new InputPoller().WithName("InputPoller").WithParent(this);

    Instance = this;

    Scheduler = new Scheduler();
    Debugger = new Debugger() { Name = "Debugger" }.WithParent(this);
    camera = new GameCamera().WithName("Camera").WithParent(this);
    SafeSpace = new SafeSpace().WithName("SafeSpace").WithParent(this);
    enemies = new Node().WithName("Enemies").WithParent(this);

    gameUI = this.GetChildOfType<GameUI>().NotNull();
  }

  private void InitializeSubComponents()
  {
    foreach (var file in DirAccess.GetFilesAt(GAME_FILES_DIR))
    {
      GD.Load<PackedScene>(Path.Join(GAME_FILES_DIR, file)).Instantiate().WithParent(this);
    }
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    Scheduler.Process();
  }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
