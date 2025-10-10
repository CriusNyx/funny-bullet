using Godot;

[GlobalClass]
public partial class PIDConfig : Resource
{
  [Export]
  public float Kp;

  [Export]
  public float Ki;

  [Export]
  public float Kd;

  [Export]
  public float integralBounds;
}
