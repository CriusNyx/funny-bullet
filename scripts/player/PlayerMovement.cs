using Godot;

[GlobalClass]
public partial class PlayerMovement : Node, Behavior
{
  [Export]
  public float playerSpeed = 10f;

  [Export]
  public float acceleration = 50f;
  private Vector2 currentVelocity = Vector2.Zero;

  public void HostUpdate(Actor host, double deltaTime)
  {
    if (host.As<Player>().WarnNull() is Player player)
    {
      PlayerUpdate(deltaTime, player);
    }
  }

  void PlayerUpdate(double delta, Player player)
  {
    var input = player.Input;
    var movementInput = input.GetMovementVector();
    var targetVelocity = movementInput * playerSpeed;
    currentVelocity = currentVelocity.MoveToward(targetVelocity, 50f);
    player.Position += (currentVelocity * (float)delta).To3();
  }
}
