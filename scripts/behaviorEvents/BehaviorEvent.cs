public enum BehaviorEventType
{
  None,
  Zenith,
  Fire,
}

public abstract class BehaviorEvent
{
  public abstract BehaviorEventType Type { get; }
}
