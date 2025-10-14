using Godot;

public partial class InputPoller : Node
{
  public static InputPoller Instance => Game.Instance.NotNull().input;
  public static InputFrame CurrentFrame => Instance.currentFrame;

  private InputFrame currentFrame = null!;

  public override void _Ready()
  {
    currentFrame = InputFrame.Empty();
  }

  public override void _Process(double delta)
  {
    currentFrame = InputFrame.Poll(currentFrame);
  }
}
