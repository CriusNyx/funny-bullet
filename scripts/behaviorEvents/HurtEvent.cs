public class HurtEvent(Hitbox hitbox, Hurtbox hurtbox) : BehaviorEvent
{
  public override BehaviorEventType Type => BehaviorEventType.Hurt;

  public Hitbox Hitbox => hitbox;
  public Hurtbox Hurtbox => hurtbox;
}
