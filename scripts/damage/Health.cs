using Godot;

[GlobalClass]
public partial class Health : Node, IHaveHealth
{
  [Export]
  public int maxHealth = 10;

  [Export]
  public int health = 10;

  public Health GetHealth()
  {
    return this;
  }

  public void Hurt()
  {
    health--;
    if (health < 0)
    {
      foreach (var parent in this.GetParentsOfType<IHandleDeath>())
      {
        parent.OnDead(this);
      }
    }
  }
}

public interface IHaveHealth
{
  public Health GetHealth();
}

public interface IHandleDeath
{
  public void OnDead(Health health);
}
