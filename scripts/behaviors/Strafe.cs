using Godot;

[GlobalClass]
public partial class Strafe : Behavior
{
  [Export]
  public float strafeAngle = 0;
  public float strafeSpeed = 10f;

  float distanceFromPlayer = Mathf.Inf;
  bool hasHitInflection = false;

  public override void _Process(double delta)
  {
    base._Process(delta);
    if (Host is BehaviorHost host)
    {
      host.Position += Vector2.Right.Rotated(strafeAngle).To3() * strafeSpeed * (float)delta;
    }

    CheckZenith();
  }

  private void CheckZenith()
  {
    if (!hasHitInflection && Host is BehaviorHost host && Player.Instance is Player player)
    {
      var distance = host.Position.DistanceTo(player.Position);
      if (distance > distanceFromPlayer)
      {
        HitZenith();
      }
      distanceFromPlayer = distance;
    }
  }

  private void HitZenith()
  {
    hasHitInflection = true;
    Host?.BroadcastEvent(new ZenithEvent { }, this);
  }
}
