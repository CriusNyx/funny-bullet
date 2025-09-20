using Godot;
using static GameStats;

[GlobalClass]
public partial class Player : Character, IHaveHealth, IHandleHurtboxEvents, IHandleDeath
{
  [Export]
  public int MyInt;
  InputFrame input = InputFrame.Empty();

  public override void _Ready()
  {
    base._Ready();
    health = this.GetChildOfType<Health>();
  }

  public Health GetHealth()
  {
    throw new System.NotImplementedException();
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    input = InputFrame.Poll(input);

    Position += input.Current.GetInputVector().To3() * (float)delta * PLAYER_SPEED;
  }

  public void OnHurt(Hitbox hitbox, Hurtbox hurtbox)
  {
    health?.Hurt();
  }

  public void OnDead(Health health)
  {
    QueueFree();
  }
}
