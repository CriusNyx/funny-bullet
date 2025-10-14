using Godot;

[GlobalClass]
public partial class PIDTest : Node3D
{
  public PIDVector3? pidController;
  public float maxAcceleration = 50f;
  public float maxVelocity = 15f;
  public Vector3 velocity = Vector3.Zero;

  Vector3 target = Vector3.Zero;

  public override void _Process(double delta)
  {
    if (Input.IsKeyPressed(Key.Left))
    {
      target = Vector3.Left * 20f;
    }

    if (Input.IsKeyPressed(Key.Right))
    {
      target = Vector3.Right * 20f;
    }
    base._Process(delta);
  }

  public override void _PhysicsProcess(double delta)
  {
    var pid = pidController?.Iterate(Position, target, (float)delta) ?? Vector3.Zero;
    DebugDraw3D.DrawRay(Position + Vector3.Up * 3f, pid, pid.Length());

    velocity = velocity.MoveToward(pid, maxAcceleration * (float)delta).SClamp(maxVelocity);
    velocity += Vector3.Left * (float)delta;

    Position += velocity * (float)delta;
  }
}
