using Godot;

[GlobalClass]
public partial class GameCamera : Camera3D
{
  public override void _Ready()
  {
    base._Ready();

    InitializeCameraParameters();
  }

  private void InitializeCameraParameters()
  {
    Position = Vector3.Back * GameStats.CAMERA_DISTANCE;
    Projection = ProjectionType.Orthogonal;
    KeepAspect = KeepAspectEnum.Height;
    Size = GameStats.GAMEBOARD_HEIGHT;
  }
}
