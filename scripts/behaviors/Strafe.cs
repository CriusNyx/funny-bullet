using Godot;

/// <summary>
/// The enemies strafes in a strait direction.
/// </summary>
[GlobalClass]
public partial class Strafe : Node, Behavior
{
  [Export]
  public float strafeAngle = 0;
  public float strafeSpeed = 10f;

  float distanceFromPlayer = Mathf.Inf;
  bool hasHitInflection = false;

  public override void _Process(double delta)
  {
    base._Process(delta);
    if (this.GetActor() is Actor host)
    {
      host.Position += Vector2.Right.Rotated(strafeAngle).To3() * strafeSpeed * (float)delta;
    }

    CheckZenith();
  }

  private void CheckZenith()
  {
    if (!hasHitInflection && this.GetActor() is Actor host && Player.Instance is Player player)
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
    this.GetActor()?.BroadcastEvent(new ZenithEvent { }, this);
  }
}
