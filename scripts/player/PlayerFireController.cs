using Godot;

[GlobalClass]
public partial class PlayerFireController : Node, Behavior
{
  [Export]
  public float FireRate = 2f;
  public double fireCooldown = -1;

  public void HostUpdate(Actor actor, double deltaTime)
  {
    if (actor.As<Player>().WarnNull() is Player player)
    {
      PlayerUpdate(player, deltaTime);
    }
  }

  private void PlayerUpdate(Player player, double deltaTime)
  {
    if (CheckFireInput(player) && CheckFireRate(deltaTime))
    {
      this.BroadcastEvent(
        new FireEvent()
        {
          fireParameters = new FireParameters { atPlayer = false, firePattern = "player" },
        }
      );
    }
  }

  private bool CheckFireInput(Player player)
  {
    return player.Input.GetInput(InputType.Fire);
  }

  private bool CheckFireRate(double delta)
  {
    fireCooldown -= delta;
    if (fireCooldown < 0)
    {
      fireCooldown = 1f / FireRate;
      return true;
    }
    return false;
  }
}
