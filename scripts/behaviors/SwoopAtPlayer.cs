using Godot;

[GlobalClass]
public partial class SwoopAtPlayer : Behavior
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

  public override void HostPhysicsUpdate(BehaviorHost host, double deltaTime)
  {
    base.HostPhysicsUpdate(host, deltaTime);
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
    var hostPosition = Host?.Position ?? default;
    var targetPosition = GetTarget(player);

    var pid = pidController?.Iterate(hostPosition, targetPosition, deltaTime) ?? Vector3.Zero;

    DebugDraw3D.DrawBox(
      targetPosition,
      Quaternion.Identity,
      Vector3.One * 0.5f,
      is_box_centered: true,
      duration: 1f
    );

    velocity = velocity.MoveToward(pid, deltaTime * acceleration).SClamp(maxVelocity);

    if (Host != null)
    {
      Host.Position += velocity * deltaTime;
    }
  }

  private Vector3 GetTarget(Player player)
  {
    var hostPosition = Host?.Position ?? default;
    var playerPosition = player.Position;

    if (hasHitZenith)
    {
      return (hostPosition - playerPosition).Normalized() * 200f;
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
      var hostPosition = Host?.Position ?? default;
      if (hostPosition.DistanceTo(target) < inflectionDistance)
      {
        HitZenith();
      }
    }
  }

  private void HitZenith()
  {
    hasHitZenith = true;
    BroadcastEvent(new ZenithEvent { });
  }
}
