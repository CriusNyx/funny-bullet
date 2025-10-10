using Godot;

public partial class PIDVector3
{
  PIDConfig config;
  Vector3 integral = Vector3.Zero;
  Vector3 previousError = Vector3.Zero;

  public PIDVector3(PIDConfig config)
  {
    this.config = config;
  }

  public Vector3 Iterate(Vector3 current, Vector3 target, float deltaTime)
  {
    // P
    var error = target - current;
    var pOut = config.Kp * error;

    // I
    integral += error * deltaTime;
    integral = integral.SClamp(config.integralBounds);

    var iOut = config.Ki * integral;

    // D
    var derivative = (error - previousError) / deltaTime;
    var dOut = config.Kd * derivative;

    previousError = error;
    return pOut + iOut + dOut;
  }
}
