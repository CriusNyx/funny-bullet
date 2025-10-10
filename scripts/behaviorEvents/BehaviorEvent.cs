public enum BehaviorEventType
{
  None,
  Zenith,
  Fire,
  Death,
  Hurt,
}

public abstract class BehaviorEvent
{
  public abstract BehaviorEventType Type { get; }
}
