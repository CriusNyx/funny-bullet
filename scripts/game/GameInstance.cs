using Godot;
using static Godot.GD;

public partial class GameInstance : Node
{
  public static GameInstance Instance { get; private set; }
  Camera3D camera;

  public override void _Ready()
  {
    Instance = this;
    base._Ready();
    camera = this.AppendChild(new Camera3D())
      .WithTransform(Vector3.Back * GameStats.CAMERA_DISTANCE);
  }
}
