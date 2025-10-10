using Godot;

[GlobalClass]
public partial class Health : Node, Behavior
{
  [Export]
  public int maxHealth = 10;

  [Export]
  public int health = 10;

  [Export]
  public bool isDead = false;

  public Health GetHealth()
  {
    return this;
  }

  public void Hurt()
  {
    GD.Print("Hurt");
    health--;
    CheckDead();
  }

  private void CheckDead()
  {
    if (!isDead && health <= 0)
    {
      isDead = true;
      this.BroadcastEvent(new DeathEvent());
    }
  }

  public void OnBehaviorEvent(BehaviorEvent e, Behavior sender)
  {
    if (e is HurtEvent he)
    {
      Hurt();
    }
  }
}
