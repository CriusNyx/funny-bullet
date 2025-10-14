using Godot;

/// <summary>
/// Swoop at the player
/// </summary>
[GlobalClass]
public partial class SwoopAtPlayer : Node, Behavior
{
  [Export]
  public PIDConfig? pidConfig;

  PIDVector3? pidController;

  [Export]
  public float targetDistance = 15f;

  [Export]
  public float inflectionDistance = 1f;

  [Export]
  bool hasHitZenith = false;

  [Export]
  public float acceleration = 50f;

  [Export]
  public float maxVelocity = 20f;

  Vector3 direction = default;
  Vector3 velocity = default;

  public override void _Ready()
  {
    pidController = new PIDVector3(pidConfig.WarnNull()!);
    base._Ready();
  }

  public void HostPhysicsUpdate(Actor host, double deltaTime)
  {
    if (Player.Instance is Player player)
    {
      var target = GetTarget(player);
      UpdatePosition(player, (float)deltaTime, target);
      CheckZenith(target);
    }
    base._PhysicsProcess(deltaTime);
  }

  private void UpdatePosition(Player player, float deltaTime, Vector3 target)
  {
    var hostPosition = this.GetActor()?.Position ?? default;
    var targetPosition = GetTarget(player);

    var pid = pidController?.Iterate(hostPosition, targetPosition, deltaTime) ?? Vector3.Zero;

    velocity = velocity.MoveToward(pid, deltaTime * acceleration).SClamp(maxVelocity);

    if (this.GetActor() is Actor actor)
    {
      actor.Position += velocity * deltaTime;
    }
  }

  private Vector3 GetTarget(Player player)
  {
    var hostPosition = this.GetActor()?.Position ?? default;
    var playerPosition = player.Position;

    if (hasHitZenith)
    {
      return (hostPosition - playerPosition).Normalized() * 300f;
    }
    else
    {
      return playerPosition + (hostPosition - playerPosition).Normalized() * targetDistance;
    }
  }

  private void CheckZenith(Vector3 target)
  {
    if (!hasHitZenith)
    {
      var hostPosition = this.GetActor()?.Position ?? default;
      if (hostPosition.DistanceTo(target) < inflectionDistance)
      {
        HitZenith();
      }
    }
  }

  private void HitZenith()
  {
    hasHitZenith = true;
    this.BroadcastEvent(new ZenithEvent { });
  }
}
